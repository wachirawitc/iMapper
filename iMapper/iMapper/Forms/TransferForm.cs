using EnvDTE;
using EnvDTE80;
using iMapper.Constance;
using iMapper.Model;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    [System.Runtime.InteropServices.Guid("F3C11D52-4077-4075-BCE0-D8EE88F5A5BE")]
    public partial class TransferForm : Form
    {
        private readonly ProjectItem projectItem;
        private readonly DTE2 dte2;

        private readonly TemporaryRepository temporaryRepository;

        public TransferForm(ProjectItem projectItem, DTE2 dte2)
        {
            InitializeComponent();
            this.projectItem = projectItem;
            this.dte2 = dte2;

            temporaryRepository = new TemporaryRepository();
        }

        private void TransferForm_Load(object sender, EventArgs e)
        {
            InitAutoComplete();
        }

        private void InitAutoComplete()
        {
            var models = temporaryRepository.GetTransfer();
            if (models.Any())
            {
                var fullNames = models.Select(x => x.FullName).ToArray();
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(fullNames);

                Source.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                Source.AutoCompleteSource = AutoCompleteSource.CustomSource;
                Source.AutoCompleteCustomSource = collection;

                Destination.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                Destination.AutoCompleteSource = AutoCompleteSource.CustomSource;
                Destination.AutoCompleteCustomSource = collection;
            }
        }

        private void ReloadClass_Click(object sender, EventArgs e)
        {
            if (dte2 != null)
            {
                var classElements = new List<ClassModel>();

                var projects = dte2.Solution.Projects.Cast<Project>();
                foreach (var project in projects)
                {
                    var projectItems = GetProjectItems(project.ProjectItems).ToList();
                    var fileCodeModel2S = projectItems
                        .Where(x => x.Kind == KindElement.PhysicalFile)
                        .Where(x => x.FileCodeModel is FileCodeModel2)
                        .Select(x => x.FileCodeModel as FileCodeModel2)
                        .ToList();
                    if (fileCodeModel2S.Any())
                    {
                        foreach (var fileCode in fileCodeModel2S)
                        {
                            var codeElements = fileCode.CodeElements;
                            foreach (CodeElement codeElement in codeElements)
                            {
                                if (codeElement.Kind == vsCMElement.vsCMElementNamespace)
                                {
                                    classElements.AddRange(codeElement.Children.Cast<CodeElement>()
                                        .Select(GetClassElement)
                                        .Where(classElement => classElement != null));
                                }
                            }
                        }
                    }
                }

                if (classElements.Any())
                {
                    temporaryRepository.SetTransfer(classElements);
                    InitAutoComplete();
                }

                string message = $"Found {classElements.Count} class.";
                MessageBox.Show(message, Text);
            }
        }

        public static IEnumerable<ProjectItem> GetProjectItems(ProjectItems projectItems)
        {
            if (projectItems != null)
            {
                foreach (ProjectItem item in projectItems)
                {
                    yield return item;

                    if (item.SubProject != null)
                    {
                        foreach (var childItem in GetProjectItems(item.SubProject.ProjectItems))
                            yield return childItem;
                    }
                    else
                    {
                        foreach (ProjectItem childItem in GetProjectItems(item.ProjectItems))
                            yield return childItem;
                    }
                }
            }
        }

        private static ClassModel GetClassElement(CodeElement element)
        {
            try
            {
                var model = new ClassModel();
                model.Name = element.Name;
                model.FullName = element.FullName;

                var elementClass = element.Kind;
                if (elementClass == vsCMElement.vsCMElementClass)
                {
                    model.Members = GetMembers(element);
                    if (model.Members.Any() == false)
                    {
                        return null;
                    }
                }

                return model;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private static List<PropertyModel> GetMembers(CodeElement element)
        {
            var model = new List<PropertyModel>();

            var elementClass = element.Kind;
            if (elementClass == vsCMElement.vsCMElementClass)
            {
                foreach (CodeElement child in element.Children)
                {
                    if (child.Kind == vsCMElement.vsCMElementProperty)
                    {
                        var property = (CodeProperty)child;

                        model.Add(new PropertyModel
                        {
                            Name = child.Name,
                            TypeFullName = property.Type.AsFullName
                        });
                    }
                }
            }

            return model;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var models = temporaryRepository.GetTransfer();

            var source = models.FirstOrDefault(x => x.FullName.Equals(Source.Text));
            var destination = models.FirstOrDefault(x => x.FullName.Equals(Destination.Text));

            if (source == null)
            {
                MessageBox.Show("Not found source class.");
            }

            if (destination == null)
            {
                MessageBox.Show("Not found destination class.");
            }

            if (source != null && destination != null)
            {
                string name = $"Map{source.Name}To{destination.Name}";

                var template = new DefaultTransferTemplate();
                template.Namespace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
                template.Name = name;
                template.Source = source;
                template.Destination = destination;
                var code = template.TransformText();

                var fileName = $"{name}.cs";
                var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
                var originalFile = $@"{destinationPath}{fileName}";

                var sourceCode = new SourceCode(fileName, code);
                var sourceFile = sourceCode.Create();

                if (File.Exists(originalFile))
                {
                    string outputFile = Temporary.Directory + $"_{fileName}";
                    if (File.Exists(outputFile))
                    {
                        File.Delete(outputFile);
                    }
                    using (File.Create(outputFile)) { }

                    string kdiffPath = @"C:\Program Files\KDiff3\kdiff3.exe";
                    string command = $"\"{sourceFile.FullName}\" \"{originalFile}\" -o \"{outputFile}\"";
                    System.Diagnostics.Process.Start(kdiffPath, command);
                }
                else
                {
                    if (File.Exists(originalFile))
                    {
                        File.Delete(originalFile);
                    }

                    projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                }

                Close();
            }
        }
    }
}
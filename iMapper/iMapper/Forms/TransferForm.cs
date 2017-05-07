﻿using EnvDTE;
using EnvDTE80;
using iMapper.Constance;
using iMapper.Extensions;
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
                    var projectItems = project.ProjectItems.GetFilesIncludeSubFolder().ToList();
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
                                        .Select(x => x.GetClassElement())
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

                var sourceCode = new SourceManage(fileName, code);
                var sourceFile = sourceCode.Create();

                var projectItemFile = projectItem.ProjectItems
                            .GetFiles()
                            .FirstOrDefault(x => x.Name == sourceFile.Name);

                if (projectItemFile != null)
                {
                    var outputFile = new FileInfo(Temporary.Directory + $"_{fileName}");
                    outputFile.DeleteIfExisting();
                    outputFile.CreateAndDispose();

                    string command = $"\"{sourceFile.FullName}\" \"{originalFile}\" -o \"{outputFile}\"";
                    var process = System.Diagnostics.Process.Start(temporaryRepository.Kdiff.FullName, command);
                    if (process != null)
                    {
                        process.WaitForExit();

                        sourceFile.DeleteIfExisting();
                        File.Copy(outputFile.FullName, sourceFile.FullName);

                        projectItemFile.Delete();
                        projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                    }
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
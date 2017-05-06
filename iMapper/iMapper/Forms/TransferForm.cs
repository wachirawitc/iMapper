using EnvDTE;
using EnvDTE80;
using iMapper.Constance;
using iMapper.Model;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class TransferForm : Form
    {
        private readonly ProjectItem projectItem;
        private readonly DTE2 dte2;

        public TransferForm(ProjectItem projectItem, DTE2 dte2)
        {
            InitializeComponent();
            this.projectItem = projectItem;
            this.dte2 = dte2;
        }

        private void TransferForm_Load(object sender, EventArgs e)
        {
        }

        private void ReloadClass_Click(object sender, EventArgs e)
        {
            if (dte2 != null)
            {
                var classElements = new List<ClassElement>();

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

        private static ClassElement GetClassElement(CodeElement element)
        {
            try
            {
                var model = new ClassElement();
                model.Name = element.Name;
                model.FullName = element.FullName;
               
                var elementClass = element.Kind;
                if (elementClass == vsCMElement.vsCMElementClass)
                {
                    model.Members = GetMembers(element);
                }

                return model;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private static List<MemberElement> GetMembers(CodeElement element)
        {
            var model = new List<MemberElement>();

            var elementClass = element.Kind;
            if (elementClass == vsCMElement.vsCMElementClass)
            {
                foreach (CodeElement child in element.Children)
                {
                    if (child.Kind == vsCMElement.vsCMElementProperty)
                    {
                        var property = (CodeProperty)child;

                        model.Add(new MemberElement
                        {
                            Element = child,
                            Type = property.Type
                        });
                    }
                }
            }

            return model;
        }
    }
}
using EnvDTE;
using EnvDTE80;
using iMapper.Forms;
using iMapper.Support;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace iMapper.Commands
{
    internal sealed class MapperCommand
    {
        public const int ConnectDatabaseCommandId = 0x0200;
        public const int MapViewModelCommandId = 0x0300;
        public const int ValidationCommandId = 0x0400;
        public const int TransferCommandId = 0x0500;

        public static readonly Guid ConnectDatabaseCommand = new Guid("89629128-c144-443f-9920-0ed1b9bc65b6");
        public static readonly Guid MapViewModelCommand = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");
        public static readonly Guid ValidationCommand = new Guid("80d2efe5-0057-4061-b0cf-0b43565e4777");
        public static readonly Guid TransferCommand = new Guid("b32a039d-3f1d-4db7-84a1-5015a677c6d5");

        private readonly Package package;

        private MapperCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            this.package = package;

            OleMenuCommandService commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var connectDatabaseCommand = new CommandID(ConnectDatabaseCommand, ConnectDatabaseCommandId);
                var connectDatabaseCommandMenuItem = new MenuCommand(ConnectDatabaseCallback, connectDatabaseCommand);
                commandService.AddCommand(connectDatabaseCommandMenuItem);

                var mapViewModelCommand = new CommandID(MapViewModelCommand, MapViewModelCommandId);
                var mapViewModelCommandMenuItem = new MenuCommand(MapViewModelCallback, mapViewModelCommand);
                commandService.AddCommand(mapViewModelCommandMenuItem);

                var validationCommand = new CommandID(ValidationCommand, ValidationCommandId);
                var validationCommandMenuItem = new MenuCommand(ValidationCallback, validationCommand);
                commandService.AddCommand(validationCommandMenuItem);

                var transferCommand = new CommandID(TransferCommand, TransferCommandId);
                var transferCommandMenuItem = new MenuCommand(TransferCallback, transferCommand);
                commandService.AddCommand(transferCommandMenuItem);
            }

            SetSolutionPath();
        }

        public static MapperCommand Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider => package;

        public static void Initialize(Package package)
        {
            Instance = new MapperCommand(package);
        }

        private void TransferCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                UIHierarchy uih = dte2.ToolWindows.SolutionExplorer;
                Array selectedItems = (Array)uih.SelectedItems;
                if (selectedItems == null || selectedItems.Length > 1)
                {
                    ShowDiabog("Select one item.", "Transfer");
                }
                else
                {
                    var hierarchyItem = selectedItems.Cast<UIHierarchyItem>().First();
                    var projectItem = hierarchyItem.Object as ProjectItem;
                    if (projectItem != null)
                    {
                        var transferForm = new TransferForm(projectItem, dte2);
                        transferForm.ShowDialog();
                    }
                }
            }
        }

        private void ConnectDatabaseCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                var form = new ConnectDatabaseForm();
                form.ShowDialog();
            }
        }

        private void ValidationCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                UIHierarchy uih = dte2.ToolWindows.SolutionExplorer;
                Array selectedItems = (Array)uih.SelectedItems;
                if (selectedItems == null || selectedItems.Length > 1)
                {
                    ShowDiabog("Select one item.", "Validation");
                }
                else
                {
                    var hierarchyItem = selectedItems.Cast<UIHierarchyItem>().First();
                    var projectItem = hierarchyItem.Object as ProjectItem;
                    if (projectItem != null)
                    {
                        var mapViewModelForm = new ValidationForm(projectItem);
                        mapViewModelForm.ShowDialog();
                    }
                }
            }
        }

        private void MapViewModelCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                UIHierarchy uih = dte2.ToolWindows.SolutionExplorer;
                Array selectedItems = (Array)uih.SelectedItems;
                if (selectedItems == null || selectedItems.Length > 1)
                {
                    ShowDiabog("Select one item.", "Model");
                }
                else
                {
                    var hierarchyItem = selectedItems.Cast<UIHierarchyItem>().First();
                    ProjectItem projectItem = hierarchyItem.Object as ProjectItem;
                    if (projectItem != null)
                    {
                        ModelForm mapViewModelForm = new ModelForm(projectItem);
                        mapViewModelForm.ShowDialog();
                    }
                }
            }
        }

        private void ShowDiabog(string message, string title)
        {
            VsShellUtilities.ShowMessageBox(
                this.ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        public static DTE2 GetActiveIde()
        {
            var dte2 = Package.GetGlobalService(typeof(DTE)) as DTE2;
            return dte2;
        }

        public static IList<Project> Projects()
        {
            Projects projects = GetActiveIde().Solution.Projects;
            List<Project> list = new List<Project>();
            var item = projects.GetEnumerator();
            while (item.MoveNext())
            {
                var project = item.Current as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                {
                    list.AddRange(GetSolutionFolderProjects(project));
                }
                else
                {
                    list.Add(project);
                }
            }

            return list;
        }

        private static IEnumerable<Project> GetSolutionFolderProjects(Project solutionFolder)
        {
            List<Project> list = new List<Project>();
            for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                var subProject = solutionFolder.ProjectItems.Item(i).SubProject;
                if (subProject == null)
                {
                    continue;
                }

                if (subProject.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                {
                    list.AddRange(GetSolutionFolderProjects(subProject));
                }
                else
                {
                    list.Add(subProject);
                }
            }
            return list;
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

        public static void SetSolutionPath()
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                string solutionDir = System.IO.Path.GetDirectoryName(dte2.Solution.FullName);
                Temporary.SolutionPath = solutionDir;
            }
        }
    }
}
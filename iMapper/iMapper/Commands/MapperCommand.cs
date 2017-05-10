using EnvDTE;
using EnvDTE80;
using iMapper.Forms;
using iMapper.Model;
using iMapper.Support;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;

namespace iMapper.Commands
{
    internal sealed class MapperCommand
    {
        private readonly Package package;

        private MapperCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            this.package = package;

            var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var commands = new List<CommandModel>
                {
                    new CommandModel(0x0202, new Guid("89629128-c144-443f-9920-0ed1b9bc65b6"), ConnectDatabaseCallback),
                    new CommandModel(0x0204, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), ModelCallback),
                    new CommandModel(0x0206, new Guid("80d2efe5-0057-4061-b0cf-0b43565e4777"), ValidationCallback),
                    new CommandModel(0x0208, new Guid("b32a039d-3f1d-4db7-84a1-5015a677c6d5"), TransferCallback),
                    new CommandModel(0x0210, new Guid("8ae90204-5920-4068-8822-5b68e57ccb03"), RepositoryCallback),
                    new CommandModel(0x0212, new Guid("97235e93-1311-44de-982e-27798e9f1270"), ModelProjNodeCallback),
                    new CommandModel(0x0214, new Guid("ef162887-333c-4e3a-b22c-0d26604d0097"), ValidationProjNodeCallback),
                    new CommandModel(0x0216, new Guid("a0d109bf-55fa-4eac-838f-c2a36e9976c3"), RepositoryProjNodeCallback),
                    new CommandModel(0x0218, new Guid("501a94a1-4dce-46b1-ab4a-84370e66d06c"), ServiceCallback),
                    new CommandModel(0x0220, new Guid("84160585-dc7f-44b2-ab42-f9e135f7dce6"), ResaveProjectCallback),
                    new CommandModel(0x0222, new Guid("c5088d7f-2809-4383-84a3-25b64104575d"), ResXResourceCallback)
                };

                foreach (var command in commands)
                {
                    var commandId = new CommandID(command.MenuGroup, command.CommandId);
                    var menuCommand = new MenuCommand(command.Event, commandId);
                    commandService.AddCommand(menuCommand);
                }
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

        private void ResXResourceCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                UIHierarchy uih = dte2.ToolWindows.SolutionExplorer;
                Array selectedItems = (Array)uih.SelectedItems;
                if (selectedItems == null || selectedItems.Length > 1)
                {
                    ShowDiabog("Select one ResX item.", "ResX Resource");
                }
                else
                {
                    var hierarchyItem = selectedItems.Cast<UIHierarchyItem>().First();
                    var projectItem = hierarchyItem.Object as ProjectItem;
                    if (projectItem != null)
                    {
                        var resXResourceFile = (string)projectItem.Properties.Item("FullPath").Value;
                        var resxInfo = new FileInfo(resXResourceFile);

                        if (string.IsNullOrEmpty(resXResourceFile) == false && resxInfo.Exists)
                        {
                            var extension = Path.GetExtension(resxInfo.FullName);
                            if (".resx".Equals(extension))
                            {
                                var form = new ResXResourceForm(resxInfo);
                                form.ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        private static void ResaveProjectCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                foreach (Project project in dte2.Solution.Projects)
                {
                    project.Save();
                }
            }
        }

        private void ServiceCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                UIHierarchy uih = dte2.ToolWindows.SolutionExplorer;
                Array selectedItems = (Array)uih.SelectedItems;
                if (selectedItems == null || selectedItems.Length > 1)
                {
                    ShowDiabog("Select one item.", "Service");
                }
                else
                {
                    var hierarchyItem = selectedItems.Cast<UIHierarchyItem>().First();
                    var projectItem = hierarchyItem.Object as ProjectItem;
                    if (projectItem != null)
                    {
                        var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
                        var nameSpace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);

                        var serviceForm = new ServiceForm(destinationPath, nameSpace, projectItem.ProjectItems);
                        serviceForm.ShowDialog();
                    }
                }
            }
        }

        private void RepositoryCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                UIHierarchy uih = dte2.ToolWindows.SolutionExplorer;
                Array selectedItems = (Array)uih.SelectedItems;
                if (selectedItems == null || selectedItems.Length > 1)
                {
                    ShowDiabog("Select one item.", "Repository");
                }
                else
                {
                    var hierarchyItem = selectedItems.Cast<UIHierarchyItem>().First();
                    var projectItem = hierarchyItem.Object as ProjectItem;
                    if (projectItem != null)
                    {
                        var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
                        var nameSpace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);

                        var repositoryForm = new RepositoryForm(destinationPath, nameSpace, projectItem.ProjectItems);
                        repositoryForm.ShowDialog();
                    }
                }
            }
        }

        private void RepositoryProjNodeCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                UIHierarchy uih = dte2.ToolWindows.SolutionExplorer;
                Array selectedItems = (Array)uih.SelectedItems;
                if (selectedItems == null || selectedItems.Length > 1)
                {
                    ShowDiabog("Select one item.", "Repository");
                }
                else
                {
                    var hierarchyItem = selectedItems.Cast<UIHierarchyItem>().First();
                    Project project = hierarchyItem.Object as Project;
                    if (project != null)
                    {
                        var destinationPath = project.Properties.Item("FullPath").Value as string;
                        var nameSpace = NamespaceHelper.Get(project);

                        var validationForm = new RepositoryForm(destinationPath, nameSpace, project.ProjectItems);
                        validationForm.ShowDialog();
                    }
                }
            }
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
                        var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
                        var nameSpace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);

                        var mapViewModelForm = new ValidationForm(destinationPath, nameSpace, projectItem.ProjectItems);
                        mapViewModelForm.ShowDialog();
                    }
                }
            }
        }

        private void ValidationProjNodeCallback(object sender, EventArgs e)
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
                    Project project = hierarchyItem.Object as Project;
                    if (project != null)
                    {
                        var destinationPath = project.Properties.Item("FullPath").Value as string;
                        var nameSpace = NamespaceHelper.Get(project);

                        var validationForm = new ValidationForm(destinationPath, nameSpace, project.ProjectItems);
                        validationForm.ShowDialog();
                    }
                }
            }
        }

        private void ModelCallback(object sender, EventArgs e)
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
                        var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
                        var nameSpace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
                        ModelForm mapViewModelForm = new ModelForm(destinationPath, nameSpace, projectItem.ProjectItems);
                        mapViewModelForm.ShowDialog();
                    }
                }
            }
        }

        private void ModelProjNodeCallback(object sender, EventArgs e)
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
                    Project project = hierarchyItem.Object as Project;
                    if (project != null)
                    {
                        var destinationPath = project.Properties.Item("FullPath").Value as string;
                        var nameSpace = NamespaceHelper.Get(project);
                        ModelForm mapViewModelForm = new ModelForm(destinationPath, nameSpace, project.ProjectItems);
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

        public static void SetSolutionPath()
        {
            var dte2 = GetActiveIde();
            if (dte2 != null)
            {
                string solutionDir = System.IO.Path.GetDirectoryName(dte2.Solution.FullName);
                Temporary.SolutionPath = solutionDir;
            }
        }
    }
}
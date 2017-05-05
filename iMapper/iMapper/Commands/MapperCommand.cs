using EnvDTE;
using EnvDTE80;
using iMapper.Constance;
using iMapper.Extensions;
using iMapper.Forms;
using iMapper.Model;
using iMapper.Support;
using iMapper.Template.ModelTemplate;
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
        public const int CommandId = 0x0100;
        public const int ConnectDatabaseCommandId = 0x0200;
        public const int MapViewModelCommandId = 0x0300;

        public static readonly Guid MapperCommandId = new Guid("a3b89b0e-701e-48e0-843b-79082041a30c");
        public static readonly Guid ConnectDatabaseCommand = new Guid("89629128-c144-443f-9920-0ed1b9bc65b6");
        public static readonly Guid MapViewModelCommand = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");

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
                var menuCommandId = new CommandID(MapperCommandId, CommandId);
                var menuItem = new MenuCommand(MenuItemCallback, menuCommandId);
                commandService.AddCommand(menuItem);

                var connectDatabaseCommand = new CommandID(ConnectDatabaseCommand, ConnectDatabaseCommandId);
                var connectDatabaseCommandMenuItem = new MenuCommand(ConnectDatabaseCallback, connectDatabaseCommand);
                commandService.AddCommand(connectDatabaseCommandMenuItem);

                var mapViewModelCommand = new CommandID(MapViewModelCommand, MapViewModelCommandId);
                var mapViewModelCommandMenuItem = new MenuCommand(MapViewModelCallback, mapViewModelCommand);
                commandService.AddCommand(mapViewModelCommandMenuItem);
            }
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

        private void ConnectDatabaseCallback(object sender, EventArgs e)
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                var form = new ConnectDatabaseForm();
                form.ShowDialog();
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
                    ShowDiabog("Select one item.", "Map view model");
                }
                else
                {
                    var hierarchyItem = selectedItems.Cast<UIHierarchyItem>().First();
                    ProjectItem projectItem = hierarchyItem.Object as ProjectItem;
                    if (projectItem != null)
                    {
                        ProjectItems projectItems = projectItem.ProjectItems;
                        MapViewModelForm mapViewModelForm = new MapViewModelForm(projectItems);
                        mapViewModelForm.ShowDialog();
                    }
                }
            }
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            MapObject();
        }

        private void MapObject()
        {
            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                var textSelection = dte2.ActiveWindow.Selection as TextSelection;

                //เลือกที่ชื่อ Class
                var selectedClass = textSelection?.ActivePoint.CodeElement[vsCMElement.vsCMElementClass] as CodeClass;
                if (selectedClass != null)
                {
                    var @interface = selectedClass.ImplementedInterfaces.OfType<CodeInterface>().FirstOrDefault();
                    if (@interface != null)
                    {
                        string interfaceName = @interface.FullName;
                        string namespaceFullName = @interface.Namespace.FullName;
                        if (string.IsNullOrEmpty(interfaceName) == false)
                        {
                            var strings = interfaceName.Replace(namespaceFullName + ".", string.Empty).Split('<', '>').ToList();
                            if (strings.Count == 3)
                            {
                                if (MapperInfo.Interface.Equals(strings[0]))
                                {
                                    var names = strings[1].Split(',').ToList()
                                        .Select(x => x.Trim())
                                        .ToList();
                                    if (names.Count == 2)
                                    {
                                        var elements = GetProjectsInSolution();
                                        var source = elements.FirstOrDefault(x => x.FullName.EndsWith(names[0]));
                                        var destination = elements.FirstOrDefault(x => x.FullName.EndsWith(names[1]));

                                        if (source != null && destination != null)
                                        {
                                            DeleteMapFunctionIfExisting(MapperInfo.Member, selectedClass);

                                            CodeFunction codeFunction = selectedClass.AddFunction(MapperInfo.Member,
                                                vsCMFunction.vsCMFunctionFunction,
                                                destination.FullName,
                                                -1,
                                                vsCMAccess.vsCMAccessPublic,
                                                null);

                                            codeFunction.AddParameter("source", source.FullName);

                                            InitMapFunctionDetail(codeFunction, new MapperModelTemplate(source, destination));

                                            ShowDiabog("Success", "iMapper");
                                        }
                                    }
                                }
                                else
                                {
                                    ShowDiabog("You must implement IMapper", "iMapper");
                                }
                            }
                        }
                    }
                    else
                    {
                        ShowDiabog("Please implement IMapper", "iMapper");
                    }
                }
            }
        }

        private static void InitMapFunctionDetail(CodeFunction codeFunction, MapperModelTemplate template)
        {
            EditPoint startPoint = codeFunction.StartPoint.CreateEditPoint();
            startPoint.LineDown(2);
            startPoint.Insert(template.GetText());
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

        private static void DeleteMapFunctionIfExisting(string functionName, CodeClass codeClass)
        {
            foreach (CodeElement child in codeClass.Children)
            {
                string name = child.Name;
                if (child.Kind == vsCMElement.vsCMElementFunction && functionName.Equals(name))
                {
                    codeClass.RemoveMember(functionName);
                }
            }
        }

        private static List<ClassElement> GetProjectsInSolution()
        {
            var model = new List<ClassElement>();

            foreach (Project project in Projects())
            {
                var directoryInfo = project.GetDirectoryInfo();
                bool isSubDirectory = Session.GetMapDirectories.Any(directory => directory.IsSubDirectoryOfOrSame(directoryInfo));
                if (isSubDirectory)
                {
                    var item = GetClass(project);
                    model.AddRange(item);
                }
            }

            return model;
        }

        private static List<ClassElement> GetClass(Project project)
        {
            var elements = new List<ClassElement>();

            var codeModel2S = GetProjectItems(project.ProjectItems)
                .Where(FilterSource)
                .Select(x => x.FileCodeModel as FileCodeModel2)
                .Where(x => x != null)
                .ToList();

            foreach (var codeElement in codeModel2S
                .SelectMany(model2 =>
                    (from CodeElement codeElement in model2.CodeElements let elementNamespace = codeElement.Kind where elementNamespace == vsCMElement.vsCMElementNamespace select codeElement)))
            {
                elements.AddRange(from CodeElement element in codeElement.Children select GetClassElement(element));
            }

            return elements.Where(x => x != null).ToList();
        }

        private static bool FilterSource(ProjectItem item)
        {
            if (item?.IsFolder() == false)
            {
                var fileInfo = item.GetFileInfo();
                if (fileInfo != null)
                {
                    if (Session.GetMapDirectories.Any(parent => fileInfo.Directory.IsSubDirectoryOfOrSame(parent)))
                    {
                        return true;
                    }
                }
            }

            return false;
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
            catch (Exception)
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
    }
}
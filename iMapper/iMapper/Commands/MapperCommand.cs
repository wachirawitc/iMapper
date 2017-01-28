using EnvDTE;
using EnvDTE80;
using iMapper.Constance;
using iMapper.Model;
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

        public static readonly Guid CommandSet = new Guid("a3b89b0e-701e-48e0-843b-79082041a30c");

        private readonly Package package;

        private MapperCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        public static MapperCommand Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public static void Initialize(Package package)
        {
            Instance = new MapperCommand(package);
        }

        private void MenuItemCallback(object sender, EventArgs e)
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
                var item = GetProjectItems(project);
                model.AddRange(item);
            }

            return model;
        }

        private static List<ClassElement> GetProjectItems(Project project)
        {
            var elements = new List<ClassElement>();

            List<FileCodeModel2> models = new List<FileCodeModel2>();
            var projectItems = project.ProjectItems.GetEnumerator();
            while (projectItems.MoveNext())
            {
                var projectItem = projectItems.Current as ProjectItem;
                if (projectItem == null)
                {
                    continue;
                }

                if (projectItem.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    var subProjectItems = GetSubItemsInFolder(projectItem);
                    var fileCodes = (from model in subProjectItems let name = model.Name select model.FileCodeModel)
                        .OfType<FileCodeModel2>()
                        .Where(x => x != null).ToList();

                    models.AddRange(fileCodes);
                }
                else
                {
                    var fileCodeModel2 = projectItem.FileCodeModel as FileCodeModel2;
                    if (fileCodeModel2 != null)
                    {
                        models.Add(fileCodeModel2);
                    }
                }
            }

            foreach (var model2 in models)
            {
                foreach (CodeElement codeElement in model2.CodeElements)
                {
                    var elementNamespace = codeElement.Kind;
                    if (elementNamespace == vsCMElement.vsCMElementNamespace)
                    {
                        elements.AddRange(from CodeElement element in codeElement.Children select GetClass(element));
                    }
                }
            }

            return elements.Where(x => x != null).ToList();
        }

        private static List<ProjectItem> GetSubItemsInFolder(ProjectItem projectItem)
        {
            List<ProjectItem> items = new List<ProjectItem>();
            for (var i = 1; i <= projectItem.ProjectItems.Count; i++)
            {
                ProjectItem item = projectItem.ProjectItems.Item(i);
                if (item == null)
                {
                    continue;
                }

                if (item.ProjectItems.Count > 0)
                {
                    items.AddRange(GetSubItemsInFolder(item));
                }
                else
                {
                    if (item.Name.EndsWith(".cs"))
                    {
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        private static ClassElement GetClass(CodeElement element)
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
    }
}
﻿using EnvDTE;
using EnvDTE80;
using iMapper.Model;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
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
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "MapperCommand";

            var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            if (dte2 != null)
            {
                var elements = GetProjectsInSolution(dte2);

                var textSelection = dte2.ActiveWindow.Selection as EnvDTE.TextSelection;

                //เลือกที่ชื่อ Class
                var selectedClass = textSelection?.ActivePoint.CodeElement[vsCMElement.vsCMElementClass] as CodeClass;
                var @interface = selectedClass?.ImplementedInterfaces.OfType<CodeInterface>().FirstOrDefault();
                if (@interface != null)
                {
                    string interfaceName = @interface.FullName;
                    string namespaceFullName = @interface.Namespace.FullName;
                    if (string.IsNullOrEmpty(interfaceName) == false)
                    {
                        var strings = interfaceName.Replace(namespaceFullName + ".", string.Empty).Split('<', '>').ToList();
                        if (strings.Count == 3)
                        {
                            var names = strings[1].Split(',').ToList()
                                .Select(x => x.Trim())
                                .ToList();
                            if (names.Count == 2)
                            {
                                var source = elements.FirstOrDefault(x => x.Name == names[0]);
                                var destination = elements.FirstOrDefault(x => x.Name == names[1]);

                                if (source != null && destination != null)
                                {
                                    int x = 0;
                                }
                            }
                        }
                    }
                }
            }

            VsShellUtilities.ShowMessageBox(
                this.ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private static List<ClassElement> GetProjectsInSolution(DTE2 dte2)
        {
            List<ClassElement> model = new List<ClassElement>();

            foreach (Project project in dte2.Solution.Projects)
            {
                var item = GetProjectItems(project);
                model.AddRange(item);
            }

            return model;
        }

        private static List<ClassElement> GetProjectItems(Project project)
        {
            List<ClassElement> elements = new List<ClassElement>();

            foreach (ProjectItem projectItem in project.ProjectItems)
            {
                var fileCodeModel2 = projectItem.FileCodeModel as FileCodeModel2;
                if (fileCodeModel2 != null && projectItem.Name.EndsWith(".cs"))
                {
                    foreach (CodeElement codeElement in fileCodeModel2.CodeElements)
                    {
                        var elementNamespace = codeElement.Kind;
                        if (elementNamespace == vsCMElement.vsCMElementNamespace)
                        {
                            elements.AddRange(from CodeElement element in codeElement.Children select GetClass(element));
                        }
                    }
                }
            }

            return elements;
        }

        private static ClassElement GetClass(CodeElement element)
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
                        CodeProperty property = (CodeProperty)child;

                        model.Add(new MemberElement
                        {
                            Name = child.Name,
                            FullName = child.FullName,
                            Type = property.Type.AsFullName
                        });
                    }
                }
            }

            return model;
        }
    }
}
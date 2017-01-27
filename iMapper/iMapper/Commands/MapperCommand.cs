using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
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
                string fullName = dte2.ActiveDocument.FullName;
                string fileName = dte2.ActiveDocument.Name;
                string language = dte2.ActiveDocument.Language;
                var selection = dte2.ActiveDocument.Selection;

                var textSelection = dte2.ActiveWindow.Selection as EnvDTE.TextSelection;

                //เลือกที่ชื่อ Class
                var selectedClass = textSelection?.ActivePoint.CodeElement[vsCMElement.vsCMElementClass] as CodeClass;
                if (selectedClass != null)
                {
                    CodeInterface @interface = selectedClass.ImplementedInterfaces.OfType<CodeInterface>().FirstOrDefault();
                    if (@interface != null)
                    {
                        string interfaceFullName = @interface.FullName;

                        if (string.IsNullOrEmpty(interfaceFullName) == false)
                        {
                            foreach (Project project in dte2.Solution.Projects)
                            {
                                foreach (ProjectItem projectItem in project.ProjectItems)
                                {
                                    string name = projectItem.Name;
                                    if (string.IsNullOrEmpty(name))
                                    {
                                    }
                                }
                            }
                        }
                    }

                    //var classBases = selectedClass.Bases;
                    //if (classBases != null)
                    //{
                    //    int count = classBases.Count;
                    //    foreach (var codeElement in classBases)
                    //    {
                    //        var baseCodeClass = codeElement as CodeClass;
                    //        if (baseCodeClass != null)
                    //        {
                    //        }
                    //    }
                    //}

                    //foreach (CodeElement elem in selectedClass.Members)
                    //{
                    //    if (elem.Kind == vsCMElement.vsCMElementProperty)
                    //    {
                    //        var name = elem.Name;
                    //    }
                    //}
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
    }
}
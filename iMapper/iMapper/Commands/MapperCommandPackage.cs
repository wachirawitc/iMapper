using EnvDTE;
using EnvDTE80;
using iMapper.Constance;
using iMapper.Extensions;
using iMapper.Repository;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

namespace iMapper.Commands
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(MapperCommandPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class MapperCommandPackage : Package
    {
        public const string PackageGuidString = "37023129-2a33-478e-8bb8-8fa2efbc2e54";

        private static DTE2 dte;

        internal static DTE2 Dte => dte ?? (dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2);

        #region Package Members

        protected override void Initialize()
        {
            base.Initialize();
            MapperCommand.Initialize(this);

            if (Dte != null)
            {
                Dte.Events.DocumentEvents.DocumentOpened += DocEventOnDocumentOpened;
                Dte.Events.DocumentEvents.DocumentSaved += DocEventOnDocumentSaved;
            }
        }

        #endregion Package Members

        #region DocumentEvents

        private static void DocEventOnDocumentOpened(Document document)
        {
            string name = document.FullName;
        }

        private static void DocEventOnDocumentSaved(Document document)
        {
            if (document.ProjectItem.Kind == KindElement.PhysicalFile)
            {
                var fileCodeModel2 = document.ProjectItem as FileCodeModel2;
                if (fileCodeModel2 != null)
                {
                    TemporaryRepository temporaryRepository = new TemporaryRepository();
                    var transfer = temporaryRepository.GetTransfer();

                    foreach (CodeElement codeElement in fileCodeModel2.CodeElements)
                    {
                        foreach (var result in codeElement.Children.Cast<CodeElement>())
                        {
                            var element = result.GetClassElement();
                            if (element != null)
                            {
                                var code = transfer.FirstOrDefault(x => x.FullName == element.FullName);
                                if (code != null)
                                {
                                    transfer.Remove(code);
                                    transfer.Add(code);
                                }
                            }
                        }
                    }

                    temporaryRepository.SetTransfer(transfer);
                }
            }
        }

        #endregion DocumentEvents
    }
}
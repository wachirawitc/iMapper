using Microsoft.VisualStudio.Shell;
using System.Diagnostics.CodeAnalysis;
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

        public MapperCommandPackage()
        {
        }

        #region Package Members

        protected override void Initialize()
        {
            MapperCommand.Initialize(this);
            base.Initialize();
        }

        #endregion Package Members
    }
}
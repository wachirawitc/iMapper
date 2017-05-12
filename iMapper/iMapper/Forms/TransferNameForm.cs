using EnvDTE;
using EnvDTE80;
using iMapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class TransferNameForm : Form
    {
        private const string InterfaceName = "IMapper";

        private readonly ProjectItems projectItems;

        public TransferNameForm(ProjectItems projectItems)
        {
            InitializeComponent();

            this.projectItems = projectItems;
        }

        private void OnLoadTransferNameForm(object sender, EventArgs e)
        {
        }

        private void OnClickLoad(object sender, EventArgs e)
        {
            var models = new List<string>();
            var items = projectItems.GetFilesIncludeSubFolder();
            foreach (var item in items)
            {
                var fileCode = item.FileCodeModel as FileCodeModel2;
                if (fileCode != null)
                {
                    var codeElements = fileCode.CodeElements;
                    foreach (CodeElement codeElement in codeElements)
                    {
                        if (codeElement.Kind == vsCMElement.vsCMElementNamespace)
                        {
                            foreach (CodeElement element in codeElement.Children)
                            {
                                if (element.Kind == vsCMElement.vsCMElementClass)
                                {
                                    var codeClass = element as CodeClass;
                                    if (codeClass != null)
                                    {
                                        var implementedInterface = codeClass.ImplementedInterfaces.OfType<CodeInterface>().FirstOrDefault();
                                        if (implementedInterface != null)
                                        {
                                            string interfaceName = implementedInterface.FullName;
                                            string namespaceFullName = implementedInterface.Namespace.FullName;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
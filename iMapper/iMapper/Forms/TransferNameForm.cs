using EnvDTE;
using EnvDTE80;
using iMapper.Extensions;
using iMapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    [System.Runtime.InteropServices.Guid("B3758C11-DDB7-417B-87F5-46CD3B624E92")]
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
            new LoadForm(() =>
            {
                Invoke((MethodInvoker)delegate
                {
                    var models = new List<TransferNameModel>();
                    var items = projectItems.GetCodeClasses();
                    foreach (var item in items)
                    {
                        foreach (var codeClass in item.CodeClasses)
                        {
                            string fileName = item.ProjectItem.Name;
                            string className = codeClass.Name;

                            var implementedInterface = codeClass.ImplementedInterfaces.OfType<CodeInterface>().FirstOrDefault();
                            if (implementedInterface != null)
                            {
                                string interfaceName = implementedInterface.FullName;
                                string namespaceFullName = implementedInterface.Namespace.FullName;
                                var strings = interfaceName.Replace(namespaceFullName + ".", string.Empty).Split('<', '>').ToList();
                                if (strings.Count == 3 && strings[1].IndexOf(',') > 0)
                                {
                                    var mapClass = strings[1].Split(',').ToList();
                                    if (mapClass.Count == 2)
                                    {
                                        var source = mapClass[0].Split('.').Last();
                                        var destination = mapClass[1].Split('.').Last();

                                        var targetClassName = $"Map{source}To{destination}";
                                        var targetFileName = $"{targetClassName}.cs";

                                        if (targetFileName.Equals(fileName) == false && targetClassName.Equals(className) == false)
                                        {
                                            models.Add(new TransferNameModel
                                            {
                                                FileName = fileName,
                                                Reason = "File name and class name is incorrect"
                                            });
                                        }
                                        else if (targetFileName.Equals(fileName) == false)
                                        {
                                            models.Add(new TransferNameModel
                                            {
                                                FileName = fileName,
                                                Reason = "File name is incorrect"
                                            });
                                        }
                                        else if (targetClassName.Equals(className) == false)
                                        {
                                            models.Add(new TransferNameModel
                                            {
                                                FileName = fileName,
                                                Reason = "Class name is incorrect"
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }

                    abnormalDatas.DataSource = models;
                });
            }).ShowDialog();
        }
    }
}
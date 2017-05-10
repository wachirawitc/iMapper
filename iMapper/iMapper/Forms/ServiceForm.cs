using EnvDTE;
using iMapper.Constance.Enumeration;
using iMapper.Extensions;
using iMapper.Model;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template.Repository;
using iMapper.Template.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class ServiceForm : Form
    {
        private readonly string destinationPath;
        private readonly string nameSpace;
        private readonly ProjectItems projectItems;
        private readonly TemporaryRepository temporaryRepository;

        public ServiceForm(string destinationPath, string nameSpace, ProjectItems projectItems)
        {
            InitializeComponent();

            this.projectItems = projectItems;
            this.destinationPath = destinationPath;
            this.nameSpace = nameSpace;
            temporaryRepository = new TemporaryRepository();
        }

        private void OnLoadServiceForm(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            var tables = temporaryRepository
                .GetColumns()
                .GroupBy(x => new { x.TableSchema, x.TableName })
                .Select(x => new ComboboxItem
                {
                    Text = $"[{x.Key.TableSchema}].[{x.Key.TableName}]",
                    Value = x.Key.TableName
                })
                .Distinct();
            foreach (var table in tables)
            {
                Tables.Items.Add(table);
            }

            var options = new List<ComboboxItem>();
            options.Add(new ComboboxItem { Text = nameof(ServiceOption.Default), Value = (int)ServiceOption.Default });
            foreach (var item in options)
            {
                Options.Items.Add(item);
            }
            Options.SelectedIndex = 0;

            var config = temporaryRepository.GetConfig() ?? new Config();
            config.Service = config.Service ?? new ServiceConfig();
            IsReplace.Checked = config.Service.IsReplace;
            Options.SelectedIndex = config.Service.OptionId;
        }

        public void SetConfig()
        {
            var config = temporaryRepository.GetConfig() ?? new Config();
            config.Service = config.Service ?? new ServiceConfig();
            config.Service.IsReplace = IsReplace.Checked;
            config.Service.OptionId = Options.SelectedIndex;
            temporaryRepository.SetConfig(config);
        }

        public string SelectTable
        {
            get
            {
                var item = Tables.SelectedItem as ComboboxItem;
                return item?.Value as string;
            }
        }

        private bool ValidationButton()
        {
            if (string.IsNullOrEmpty(SelectTable))
            {
                MessageBox.Show("Select table.", Text);
                return true;
            }
            return false;
        }

        private void OnClickSaveInterface(object sender, EventArgs e)
        {
            if (ValidationButton()) return;

            var columns = temporaryRepository
                .GetColumns()
                .Where(x => x.TableName.Equals(SelectTable))
                .ToList();

            var name = $"I{SelectTable}Service";
            var fileName = $"{name}.cs";
            var originalFile = new FileInfo($@"{destinationPath}{fileName}");

            var template = new DefaultInterfaceServiceTemplate();
            template.Name = name;
            template.TableName = SelectTable;
            template.Columns = columns;
            template.Namespace = nameSpace;

            var sourceManage = new SourceManage(fileName, template.TransformText());
            var sourceFile = sourceManage.Create();

            CreateFile(sourceFile, fileName, originalFile);

            SetConfig();
            Close();
        }

        private void OnClickSaveImplement(object sender, EventArgs e)
        {
            if (ValidationButton()) return;

            var columns = temporaryRepository
                .GetColumns()
                .Where(x => x.TableName.Equals(SelectTable))
                .ToList();

            var name = $"{SelectTable}Service";
            var fileName = $"{name}.cs";
            var originalFile = new FileInfo($@"{destinationPath}{fileName}");

            var template = new DefaultServiceTemplate();
            template.Name = name;
            template.TableName = SelectTable;
            template.Columns = columns;
            template.Namespace = nameSpace;

            var sourceManage = new SourceManage(fileName, template.TransformText());
            var sourceFile = sourceManage.Create();

            CreateFile(sourceFile, fileName, originalFile);

            SetConfig();
            Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateFile(FileInfo sourceFile, string fileName, FileInfo originalFile)
        {
            var fileInProject = projectItems
                .GetFiles()
                .FirstOrDefault(x => x.Name == sourceFile.Name);

            if (fileInProject != null && IsReplace.Checked == false)
            {
                var outputFile = new FileInfo(Temporary.Directory + $"_{fileName}");
                outputFile.DeleteIfExisting();
                outputFile.CreateAndDispose();

                var beforeModificationDate = outputFile.GetLastWriteTime();

                string command = $"\"{sourceFile.FullName}\" \"{originalFile.FullName}\" -o \"{outputFile.FullName}\"";
                var process = System.Diagnostics.Process.Start(temporaryRepository.Kdiff.FullName, command);
                if (process != null)
                {
                    process.WaitForExit();

                    var afterModificationDate = outputFile.GetLastWriteTime();
                    if (beforeModificationDate.IsEarlierThan(afterModificationDate))
                    {
                        sourceFile.DeleteIfExisting();
                        File.Copy(outputFile.FullName, sourceFile.FullName);

                        fileInProject.Delete();
                        projectItems.AddFromFileCopy(sourceFile.FullName);
                        projectItems.ContainingProject.Save();
                    }
                }
            }
            else
            {
                originalFile.DeleteIfExisting();
                projectItems.AddFromFileCopy(sourceFile.FullName);
                projectItems.ContainingProject.Save();
            }
        }
    }
}
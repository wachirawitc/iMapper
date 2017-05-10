using EnvDTE;
using Humanizer;
using iMapper.Constance.Enumeration;
using iMapper.Extensions;
using iMapper.Model;
using iMapper.Model.Database;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    [System.Runtime.InteropServices.Guid("03394275-34B7-403D-8A5E-CEDF45CFE71A")]
    public partial class ModelForm : Form
    {
        private readonly string destinationPath;
        private readonly string nameSpace;
        private readonly ProjectItems projectItem;
        private readonly TemporaryRepository temporaryRepository;

        public ModelForm(string destinationPath, string nameSpace, ProjectItems projectItem)
        {
            InitializeComponent();
            this.projectItem = projectItem;
            this.destinationPath = destinationPath;
            this.nameSpace = nameSpace;
            temporaryRepository = new TemporaryRepository();
        }

        private void OnLoadModelForm(object sender, EventArgs e)
        {
            Init();

            var config = temporaryRepository.GetConfig() ?? new Config();
            config.Model = config.Model ?? new ModelConfig();

            IsReplace.Checked = config.Model.IsReplace;
            IsPascalize.Checked = config.Model.IsPascalize;
            Options.SelectedIndex = config.Model.OptionId;
            ResXResourceName.Text = config.Model.ResXResourceName;
        }

        private void OnClickSaveButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ResXResourceName.Text))
            {
                MessageBox.Show("ResX resource name required.", Text);
                return;
            }

            var table = Tables.SelectedItem as ComboboxItem;
            if (table?.Value == null)
            {
                MessageBox.Show("Table is required.", Text);
                return;
            }

            var columns = temporaryRepository
                .GetColumns()
                .Where(x => x.TableName == (string)table.Value)
                .ToList();

            var code = GetCode(columns);

            var fileName = $"{FileName.Text}.cs";

            var originalFile = new FileInfo($@"{destinationPath}{fileName}");

            var sourceManage = new SourceManage(fileName, code);
            var sourceFile = sourceManage.Create();

            var fileInProject = projectItem
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
                        projectItem.AddFromFileCopy(sourceFile.FullName);
                        projectItem.ContainingProject.Save();
                    }
                }
            }
            else
            {
                originalFile.DeleteIfExisting();
                projectItem.AddFromFileCopy(sourceFile.FullName);
                projectItem.ContainingProject.Save();
            }

            UpdateConfig();
            Close();
        }

        private void OnSelectedTables(object sender, EventArgs e)
        {
            var item = Tables.SelectedItem as ComboboxItem;
            if (item != null)
            {
                FileName.Text = $"{item.Value.ToString().Pascalize()}Model";

                var columns = temporaryRepository
                    .GetColumns()
                    .Where(x => x.TableName == (string)item.Value)
                    .ToList();
                if (columns.Any())
                {
                    Columns.DataSource = columns;
                    ColumnNumber.Text = $"{columns.Count} Columns";
                }
            }
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
            options.Add(new ComboboxItem { Text = nameof(ModelOption.Default), Value = (int)ModelOption.Default });
            options.Add(new ComboboxItem { Text = nameof(ModelOption.AspMvc), Value = (int)ModelOption.AspMvc });
            options.Add(new ComboboxItem { Text = nameof(ModelOption.AspMvcCustom1), Value = (int)ModelOption.AspMvcCustom1 });
            foreach (var item in options)
            {
                Options.Items.Add(item);
            }
            Options.SelectedIndex = 0;

            var config = temporaryRepository.GetConfig() ?? new Config();
            config.Model = config.Model ?? new ModelConfig();
            IsReplace.Checked = config.Model.IsReplace;
            IsPascalize.Checked = config.Model.IsPascalize;
            Options.SelectedIndex = config.Model.OptionId;
        }

        private ModelOption ModelOption
        {
            get
            {
                var value = Options.SelectedItem as ComboboxItem;
                if (value == null)
                {
                    return ModelOption.Default;
                }
                return (ModelOption)(int)value.Value;
            }
        }

        private string GetCode(List<ColumnModel> columns)
        {
            string code = string.Empty;
            if (ModelOption == ModelOption.Default)
            {
                var template = new DefaultModelTemplate();
                template.IsPascalize = IsPascalize.Checked;
                template.Namespace = nameSpace;
                template.Name = FileName.Text;
                template.Columns = columns;
                code = template.TransformText();
            }
            else if (ModelOption == ModelOption.AspMvc)
            {
                var template = new AspMvcModelTemplate();
                template.IsPascalize = IsPascalize.Checked;
                template.Namespace = nameSpace;
                template.Name = FileName.Text;
                template.Columns = columns;
                template.ResXResourceName = ResXResourceName.Text;
                code = template.TransformText();
            }
            else if (ModelOption == ModelOption.AspMvcCustom1)
            {
                var template = new AspMvcModelTemplateCustom1();
                template.IsPascalize = IsPascalize.Checked;
                template.Namespace = nameSpace;
                template.Name = FileName.Text;
                template.Columns = columns;
                template.ResXResourceName = ResXResourceName.Text;
                code = template.TransformText();
            }
            return code;
        }

        private void OnSelectedOptions(object sender, EventArgs e)
        {
            var option = Options.SelectedItem as ComboboxItem;
            var table = Tables.SelectedItem as ComboboxItem;
            if (option != null && table != null)
            {
                var viewModelOption = (ModelOption)(int)option.Value;
                if (viewModelOption == ModelOption.Default)
                {
                    FileName.Text = $"{table.Value.ToString().Pascalize()}Model";
                }
                else if (viewModelOption == ModelOption.AspMvc)
                {
                    FileName.Text = $"{table.Value.ToString().Pascalize()}ViewModel";
                }
                else if (viewModelOption == ModelOption.AspMvcCustom1)
                {
                    FileName.Text = $"{table.Value.ToString().Pascalize()}ViewModel";
                }
            }
        }

        private void UpdateConfig()
        {
            var config = temporaryRepository.GetConfig() ?? new Config();
            config.Model = config.Model ?? new ModelConfig();
            config.Model.IsReplace = IsReplace.Checked;
            config.Model.IsPascalize = IsPascalize.Checked;
            config.Model.OptionId = Options.SelectedIndex;
            config.Model.ResXResourceName = ResXResourceName.Text;
            temporaryRepository.SetConfig(config);
        }
    }
}
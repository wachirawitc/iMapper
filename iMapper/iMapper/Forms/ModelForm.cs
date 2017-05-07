using EnvDTE;
using Humanizer;
using iMapper.Constance.Enumeration;
using iMapper.Extensions;
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
        private readonly ProjectItem projectItem;
        private readonly TemporaryRepository temporaryRepository;

        public ModelForm(ProjectItem projectItem)
        {
            InitializeComponent();
            this.projectItem = projectItem;
            temporaryRepository = new TemporaryRepository();
        }

        private void OnLoadModelForm(object sender, EventArgs e)
        {
            Init();
        }

        private void OnClickSaveButton(object sender, EventArgs e)
        {
            var item = Tables.SelectedItem as ComboboxItem;
            if (item != null)
            {
                var columns = temporaryRepository
                    .GetColumns()
                    .Where(x => x.TableName == (string)item.Value)
                    .ToList();

                var code = GetCode(columns);

                var fileName = $"{FileName.Text}.cs";
                var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
                var originalFile = new FileInfo($@"{destinationPath}{fileName}");

                var sourceManage = new SourceManage(fileName, code);
                var sourceFile = sourceManage.Create();

                var fileInProject = projectItem.ProjectItems
                            .GetFiles()
                            .FirstOrDefault(x => x.Name == sourceFile.Name);

                if (fileInProject != null && IsReplace.Checked == false)
                {
                    var outputFile = new FileInfo(Temporary.Directory + $"_{fileName}");
                    outputFile.DeleteIfExisting();
                    outputFile.CreateAndDispose();

                    string command = $"\"{sourceFile.FullName}\" \"{originalFile.FullName}\" -o \"{outputFile.FullName}\"";
                    var process = System.Diagnostics.Process.Start(temporaryRepository.Kdiff.FullName, command);
                    if (process != null)
                    {
                        process.WaitForExit();

                        sourceFile.DeleteIfExisting();
                        File.Copy(outputFile.FullName, sourceFile.FullName);

                        fileInProject.Delete();
                        projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                        projectItem.ContainingProject.Save();
                    }
                }
                else
                {
                    originalFile.DeleteIfExisting();
                    projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                    projectItem.ContainingProject.Save();
                }
            }

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

        private void OnClickClose(object sender, EventArgs e)
        {
            Close();
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
                template.Namespace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
                template.Name = FileName.Text;
                template.Columns = columns;
                code = template.TransformText();
            }
            else if (ModelOption == ModelOption.AspMvc)
            {
                var template = new AspMvcModelTemplate();
                template.IsPascalize = IsPascalize.Checked;
                template.Namespace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
                template.Name = FileName.Text;
                template.Columns = columns;
                code = template.TransformText();
            }
            else if (ModelOption == ModelOption.AspMvcCustom1)
            {
                var template = new AspMvcModelTemplateCustom1();
                template.IsPascalize = IsPascalize.Checked;
                template.Namespace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
                template.Name = FileName.Text;
                template.Columns = columns;
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
    }
}
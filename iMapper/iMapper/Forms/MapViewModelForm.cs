using EnvDTE;
using iMapper.Constance.Enumeration;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    [System.Runtime.InteropServices.Guid("03394275-34B7-403D-8A5E-CEDF45CFE71A")]
    public partial class MapViewModelForm : Form
    {
        private readonly ProjectItem projectItem;
        private readonly TemporaryRepository temporaryRepository;

        public MapViewModelForm(ProjectItem projectItem)
        {
            InitializeComponent();
            this.projectItem = projectItem;
            temporaryRepository = new TemporaryRepository();
        }

        private void MapViewModelForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var item = Tables.SelectedItem as ComboboxItem;
            if (item != null)
            {
                var columns = temporaryRepository
                    .GetColumns()
                    .Where(x => x.TableName == (string)item.Value)
                    .ToList();

                var template = new DefaultViewModel();
                template.Namespace = "Test";
                template.Name = FileName.Text;
                template.Columns = columns;

                var fileName = $"{FileName.Text}.cs";
                var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
                var originalFile = $@"{destinationPath}{fileName}";

                var source = new SourceCode(fileName, template.TransformText());
                var sourceFile = source.Create();

                if (File.Exists(originalFile) && IsReplace.Checked == false)
                {
                    string outputFile = Temporary.Directory + $"_{fileName}";
                    if (File.Exists(outputFile))
                    {
                        File.Delete(outputFile);
                    }
                    using (File.Create(outputFile)) { }

                    string kdiffPath = @"C:\Program Files\KDiff3\kdiff3.exe";
                    string command = $"\"{sourceFile.FullName}\" \"{originalFile}\" -o \"{outputFile}\"";
                    System.Diagnostics.Process.Start(kdiffPath, command);
                }
                else
                {
                    if (File.Exists(originalFile))
                    {
                        File.Delete(originalFile);
                    }

                    projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                }
            }

            Close();
        }

        private void Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = Tables.SelectedItem as ComboboxItem;
            if (item != null)
            {
                FileName.Text = $"{item.Value}ViewModel";

                var columns = temporaryRepository
                    .GetColumns()
                    .Where(x => x.TableName == (string)item.Value)
                    .ToList();
                if (columns.Any())
                {
                    Columns.DataSource = columns;
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
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
            options.Add(new ComboboxItem { Text = nameof(ViewModelOption.Default), Value = (int)ViewModelOption.Default });
            options.Add(new ComboboxItem { Text = nameof(ViewModelOption.AspMvc), Value = (int)ViewModelOption.AspMvc });
            options.Add(new ComboboxItem { Text = nameof(ViewModelOption.FluentValidation), Value = (int)ViewModelOption.FluentValidation });
            foreach (var item in options)
            {
                Options.Items.Add(item);
            }
            Options.SelectedIndex = 0;
        }
    }
}
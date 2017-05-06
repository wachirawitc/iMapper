using EnvDTE;
using iMapper.Constance.Enumeration;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using iMapper.Template.ViewModel;

namespace iMapper.Forms
{
    public partial class MapViewModelForm : Form
    {
        private readonly ProjectItems projectItems;
        private readonly TemporaryRepository temporaryRepository;

        public MapViewModelForm(ProjectItems projectItems)
        {
            InitializeComponent();
            this.projectItems = projectItems;

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

                var source = new SourceCode(FileName.Text, template.TransformText());
                var sourceFile = source.Create();
                projectItems.AddFromFileCopy(sourceFile.FullName);
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
using EnvDTE;
using iMapper.Constance.Enumeration;
using iMapper.Model;
using iMapper.Repository;
using iMapper.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class ServiceForm : Form
    {
        private readonly string destinationPath;
        private readonly string nameSpace;
        private readonly ProjectItems projectItem;
        private readonly TemporaryRepository temporaryRepository;

        public ServiceForm(string destinationPath, string nameSpace, ProjectItems projectItem)
        {
            InitializeComponent();

            this.projectItem = projectItem;
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
            options.Add(new ComboboxItem { Text = nameof(ServiceOption.Custom1), Value = (int)ServiceOption.Custom1 });
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

            SetConfig();
            Close();
        }

        private void OnClickSaveImplement(object sender, EventArgs e)
        {
            if (ValidationButton()) return;

            SetConfig();
            Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
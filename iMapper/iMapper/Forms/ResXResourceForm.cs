using Humanizer;
using iMapper.Model;
using iMapper.Repository;
using iMapper.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class ResXResourceForm : Form
    {
        private readonly FileInfo resourceFile;
        private readonly TemporaryRepository temporaryRepository;

        public ResXResourceForm(FileInfo resourceFile)
        {
            InitializeComponent();

            this.resourceFile = resourceFile;
            temporaryRepository = new TemporaryRepository();
        }

        private void OnLoadResXResourceForm(object sender, EventArgs e)
        {
            Init();
        }

        private void OnSelectedTableChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Table) == false)
            {
                var tables = temporaryRepository
                    .GetColumns()
                    .Where(x => x.TableName.Equals(Table))
                    .ToList();

                if (tables.Any())
                {
                    var resources = tables
                        .Select(x => new ResXResourceModel
                        {
                            Key = x.ColumnName.Pascalize(),
                            Value = x.ColumnName.Humanize(LetterCasing.Title)
                        })
                        .ToList();

                    resourceGrid.DataSource = resources;
                }
            }
        }

        private void OnClickSaveButton(object sender, EventArgs e)
        {
            using (var resXResourceWriter = new ResXResourceWriter(resourceFile.FullName))
            {
                var items = resourceGrid.DataSource as List<ResXResourceModel>;
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        resXResourceWriter.AddResource(item.Key, item.Value);
                    }
                }
            }
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
        }

        private string Table
        {
            get
            {
                var item = Tables.SelectedItem as ComboboxItem;
                return item?.Value as string;
            }
        }
    }
}
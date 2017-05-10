using Humanizer;
using iMapper.Model;
using iMapper.Repository;
using iMapper.Support;
using System;
using System.Collections;
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
        private List<ResXResourceModel> entries;

        public ResXResourceForm(FileInfo resourceFile)
        {
            InitializeComponent();

            this.resourceFile = resourceFile;
            temporaryRepository = new TemporaryRepository();
        }

        private void OnLoadResXResourceForm(object sender, EventArgs e)
        {
            entries = LoadResources();
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
                    var model = new List<ResXResourceModel>();

                    foreach (var column in tables)
                    {
                        var item = new ResXResourceModel();
                        item.Key = column.ColumnName.Pascalize();
                        item.NewValue = column.ColumnName.Humanize(LetterCasing.Title);

                        var entry = entries.FirstOrDefault(x => x.Key == item.Key);
                        item.IsExisting = entry != null;
                        item.OldValue = entry?.OldValue;

                        model.Add(item);
                    }

                    resourceGrid.DataSource = model;
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
                        if (item.IsExisting)
                        {
                            if (item.OldValue != item.NewValue && item.NewValue != item.Key.Humanize(LetterCasing.Title))
                            {
                                resXResourceWriter.AddResource(item.Key, item.NewValue);
                            }
                        }
                        else
                        {
                            resXResourceWriter.AddResource(item.Key, item.NewValue);
                        }
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

        private List<ResXResourceModel> LoadResources()
        {
            var model = new List<ResXResourceModel>();
            using (var reader = new ResXResourceReader(resourceFile.FullName))
            {
                foreach (DictionaryEntry entry in reader)
                {
                    model.Add(new ResXResourceModel
                    {
                        Key = entry.Key as string,
                        OldValue = entry.Value as string
                    });
                }

                return model;
            }
        }
    }
}
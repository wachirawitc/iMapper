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
            ResourceFileName.Text = resourceFile.Name;
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

                var keyColumn = resourceGrid.Columns["Key"];
                if (keyColumn != null)
                {
                    keyColumn.ReadOnly = true;
                    keyColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                var isExistingColumn = resourceGrid.Columns["IsExisting"];
                if (isExistingColumn != null)
                {
                    isExistingColumn.ReadOnly = true;
                    isExistingColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                }

                var replaceOldValueColumn = resourceGrid.Columns["ReplaceOldValue"];
                if (replaceOldValueColumn != null)
                {
                    replaceOldValueColumn.ReadOnly = false;
                    replaceOldValueColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                }
            }
        }

        private void OnClickSaveButton(object sender, EventArgs e)
        {
            var dictionaries = new Dictionary<string, string>();
            entries.ForEach(x => dictionaries[x.Key] = x.OldValue);

            var dataSources = resourceGrid.DataSource as List<ResXResourceModel> ?? new List<ResXResourceModel>();
            foreach (var source in dataSources)
            {
                if (dictionaries.ContainsKey(source.Key))
                {
                    if (source.IsExisting && source.ReplaceOldValue)
                    {
                        dictionaries[source.Key] = source.NewValue;
                    }
                }
                else
                {
                    dictionaries[source.Key] = source.NewValue;
                }
            }

            using (var resXResourceWriter = new ResXResourceWriter(resourceFile.FullName))
            {
                foreach (var dictionary in dictionaries)
                {
                    resXResourceWriter.AddResource(dictionary.Key, dictionary.Value);
                }

                resXResourceWriter.Generate();
                resXResourceWriter.Dispose();
                resXResourceWriter.Close();
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
            using (var resXResourceReader = new ResXResourceReader(resourceFile.FullName))
            {
                foreach (DictionaryEntry entry in resXResourceReader)
                {
                    if (model.Any(x => x.Key == entry.Key) == false)
                    {
                        model.Add(new ResXResourceModel
                        {
                            Key = entry.Key as string,
                            OldValue = entry.Value as string
                        });
                    }
                }

                return model;
            }
        }
    }
}
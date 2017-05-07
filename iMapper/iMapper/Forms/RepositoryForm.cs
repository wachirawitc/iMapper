using EnvDTE;
using iMapper.Constance.Enumeration;
using iMapper.Repository;
using iMapper.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class RepositoryForm : Form
    {
        private readonly ProjectItem projectItem;
        private readonly TemporaryRepository temporaryRepository;

        public RepositoryForm(ProjectItem projectItem)
        {
            InitializeComponent();

            this.projectItem = projectItem;
            temporaryRepository = new TemporaryRepository();
        }

        private void OnLoadRepositoryForm(object sender, EventArgs e)
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
            options.Add(new ComboboxItem { Text = nameof(RepositoryOption.EntityFramework), Value = (int)RepositoryOption.EntityFramework });
            foreach (var item in options)
            {
                Options.Items.Add(item);
            }
            Options.SelectedIndex = 0;
        }

        private void OnSelectedTables(object sender, EventArgs e)
        {
            var item = Tables.SelectedItem as ComboboxItem;
            if (item != null)
            {
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
        }

        private void OnClickSave(object sender, EventArgs e)
        {
        }
    }
}
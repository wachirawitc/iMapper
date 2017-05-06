using EnvDTE;
using Humanizer;
using iMapper.Constance.Enumeration;
using iMapper.Repository;
using iMapper.Support;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class ValidationForm : Form
    {
        private readonly ProjectItem projectItem;
        private readonly TemporaryRepository temporaryRepository;

        public ValidationForm(ProjectItem projectItem)
        {
            InitializeComponent();
            this.projectItem = projectItem;
            temporaryRepository = new TemporaryRepository();
        }

        private void ValidationForm_Load(object sender, System.EventArgs e)
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
            options.Add(new ComboboxItem { Text = nameof(ValidationOption.FluentValidation), Value = (int)ValidationOption.FluentValidation });
            options.Add(new ComboboxItem { Text = nameof(ValidationOption.Custom1), Value = (int)ValidationOption.Custom1 });
            foreach (var item in options)
            {
                Options.Items.Add(item);
            }
            Options.SelectedIndex = 0;
        }

        private void Tables_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var columns = temporaryRepository
                .GetColumns()
                .Where(x => x.TableName.Equals(TableName))
                .ToList();
            Columns.DataSource = columns;
            InitTableName();
        }

        private void Options_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            InitTableName();
        }

        private void InitTableName()
        {
            if (string.IsNullOrEmpty(TableName))
            {
                return;
            }

            switch (ValidationOption)
            {
                case ValidationOption.FluentValidation:
                    FileName.Text = $"{TableName.Pascalize()}Validate";
                    break;

                case ValidationOption.Custom1:
                    FileName.Text = $"{TableName.Pascalize()}Investigate";
                    break;
            }
        }

        private string TableName
        {
            get
            {
                var value = Tables.SelectedItem as ComboboxItem;
                return value?.Value as string;
            }
        }

        private ValidationOption ValidationOption
        {
            get
            {
                var value = Options.SelectedItem as ComboboxItem;
                if (value == null)
                {
                    return ValidationOption.FluentValidation;
                }
                return (ValidationOption)(int)value.Value;
            }
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
        }
    }
}
using EnvDTE;
using Humanizer;
using iMapper.Constance.Enumeration;
using iMapper.Model.Database;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template.Validation;
using System.Collections.Generic;
using System.IO;
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

            ValidatorName.ReadOnly = true;

            switch (ValidationOption)
            {
                case ValidationOption.FluentValidation:
                    FileName.Text = $"{TableName.Pascalize()}Validate";
                    ValidatorName.Text = $"{TableName.Pascalize()}Model";
                    ValidatorName.ReadOnly = false;
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
            if (string.IsNullOrEmpty(TableName))
            {
                return;
            }

            var columns = temporaryRepository
                    .GetColumns()
                    .Where(x => x.TableName == TableName)
                    .ToList();

            var code = GetCode(columns);

            var fileName = $"{FileName.Text}.cs";
            var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
            var originalFile = $@"{destinationPath}{fileName}";

            var source = new SourceCode(fileName, code);
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
            Close();
        }

        private string GetCode(List<ColumnModel> columns)
        {
            string code = string.Empty;
            if (ValidationOption == ValidationOption.FluentValidation)
            {
                var template = new FluentValidationValidationTemplate();
                template.IsPascalize = IsPascalize.Checked;
                template.Namespace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
                template.Name = FileName.Text;
                template.ValidatorName = ValidatorName.Text;
                template.Columns = columns;
                code = template.TransformText();
            }
            return code;
        }
    }
}
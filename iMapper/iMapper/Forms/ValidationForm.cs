using EnvDTE;
using Humanizer;
using iMapper.Constance.Enumeration;
using iMapper.Extensions;
using iMapper.Model;
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
        private readonly string destinationPath;
        private readonly string nameSpace;
        private readonly ProjectItems projectItems;
        private readonly TemporaryRepository temporaryRepository;

        public ValidationForm(string destinationPath, string nameSpace, ProjectItems projectItems)
        {
            InitializeComponent();
            this.destinationPath = destinationPath;
            this.nameSpace = nameSpace;
            this.projectItems = projectItems;
            temporaryRepository = new TemporaryRepository();
        }

        private void OnLoadValidationForm(object sender, System.EventArgs e)
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

            var config = temporaryRepository.GetConfig() ?? new Config();
            config.Validation = config.Validation ?? new ValidationConfig();
            IsReplace.Checked = config.Validation.IsReplace;
            IsPascalize.Checked = config.Validation.IsPascalize;
            Options.SelectedIndex = config.Validation.OptionId;
        }

        private void OnSelectedTables(object sender, System.EventArgs e)
        {
            var columns = temporaryRepository
                .GetColumns()
                .Where(x => x.TableName.Equals(TableName))
                .ToList();
            Columns.DataSource = columns;
            InitTableName();
        }

        private void OnSelectedOptions(object sender, System.EventArgs e)
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
                    ValidatorName.Text = $"{TableName.Pascalize()}Model";
                    ValidatorName.ReadOnly = false;
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

        private void OnClickClose(object sender, System.EventArgs e)
        {
            Close();
        }

        private void OnClickSave(object sender, System.EventArgs e)
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
            var originalFile = $@"{destinationPath}{fileName}";

            var source = new SourceManage(fileName, code);
            var sourceFile = source.Create();

            var projectItemFile = projectItems
                        .GetFiles()
                        .FirstOrDefault(x => x.Name == sourceFile.Name);

            if (projectItemFile != null && IsReplace.Checked == false)
            {
                var outputFile = new FileInfo(Temporary.Directory + $"_{fileName}");
                outputFile.DeleteIfExisting();
                outputFile.CreateAndDispose();

                string command = $"\"{sourceFile.FullName}\" \"{originalFile}\" -o \"{outputFile}\"";
                var process = System.Diagnostics.Process.Start(temporaryRepository.Kdiff.FullName, command);
                if (process != null)
                {
                    process.WaitForExit();

                    sourceFile.DeleteIfExisting();
                    File.Copy(outputFile.FullName, sourceFile.FullName);

                    projectItemFile.Delete();
                    projectItems.AddFromFileCopy(sourceFile.FullName);
                    projectItems.ContainingProject.Save();
                }
            }
            else
            {
                if (File.Exists(originalFile))
                {
                    File.Delete(originalFile);
                }

                projectItems.AddFromFileCopy(sourceFile.FullName);
                projectItems.ContainingProject.Save();
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
                template.Namespace = nameSpace;
                template.Name = FileName.Text;
                template.ValidatorName = ValidatorName.Text;
                template.Columns = columns;
                code = template.TransformText();
            }
            else if (ValidationOption == ValidationOption.Custom1)
            {
                var template = new Custom1ValidationTemplate();
                template.IsPascalize = IsPascalize.Checked;
                template.Namespace = nameSpace;
                template.Name = FileName.Text;
                template.ValidatorName = ValidatorName.Text;
                template.Columns = columns;
                code = template.TransformText();
            }
            return code;
        }

        public void UpdateConfig()
        {
            var config = temporaryRepository.GetConfig() ?? new Config();
            config.Validation = config.Validation ?? new ValidationConfig();
            config.Validation.IsReplace = IsReplace.Checked;
            config.Validation.IsPascalize = IsPascalize.Checked;
            config.Validation.OptionId = Options.SelectedIndex;

            temporaryRepository.SetConfig(config);
        }
    }
}
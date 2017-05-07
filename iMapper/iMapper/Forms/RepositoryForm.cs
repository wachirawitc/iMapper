using EnvDTE;
using iMapper.Constance.Enumeration;
using iMapper.Extensions;
using iMapper.Model;
using iMapper.Model.Database;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class RepositoryForm : Form
    {
        private readonly string destinationPath;
        private readonly string nameSpace;

        private readonly ProjectItems projectItems;
        private readonly TemporaryRepository temporaryRepository;

        public RepositoryForm(string destinationPath, string nameSpace, ProjectItems projectItems)
        {
            InitializeComponent();
            this.destinationPath = destinationPath;
            this.nameSpace = nameSpace;
            this.projectItems = projectItems;
            temporaryRepository = new TemporaryRepository();
        }

        private void OnLoadRepositoryForm(object sender, EventArgs e)
        {
            Init();

            var config = temporaryRepository.GetConfig() ?? new Config();

            EntityFrameworkName.Text = config.Repository?.EntityName;
            IsPluralize.Checked = config.Repository?.IsPluralizeTable ?? false;
            IsReplace.Checked = config.Repository?.IsReplace ?? false;
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
            Close();
        }

        private void OnClickInterfaceSave(object sender, EventArgs e)
        {
            if (ValidationButton()) return;

            UpdateConfig();

            var columns = temporaryRepository
                .GetColumns()
                .Where(x => x.TableName.Equals(SelectTable))
                .ToList();

            CreateEfInterfaceRepository(columns);

            Close();
        }

        private void UpdateConfig()
        {
            var config = temporaryRepository.GetConfig() ?? new Config();
            config.Repository.EntityName = EntityFrameworkName.Text;
            config.Repository.IsPluralizeTable = IsPluralize.Checked;
            config.Repository.IsReplace = IsReplace.Checked;
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

        public void CreateEfRepository(List<ColumnModel> columns)
        {
            var name = $"{SelectTable}Repository";
            var fileName = $"{name}.cs";
            var originalFile = new FileInfo($@"{destinationPath}{fileName}");

            var template = new EntityRepositoryTemplate();
            template.Name = name;
            template.TableName = SelectTable;
            template.Columns = columns;
            template.Namespace = nameSpace;
            template.IsPluralize = IsPluralize.Checked;
            template.EntityName = EntityFrameworkName.Text;

            var sourceManage = new SourceManage(fileName, template.TransformText());
            var sourceFile = sourceManage.Create();

            CreateFile(sourceFile, fileName, originalFile);
        }

        public void CreateEfInterfaceRepository(List<ColumnModel> columns)
        {
            var name = $"I{SelectTable}Repository";
            var fileName = $"{name}.cs";
            var originalFile = new FileInfo($@"{destinationPath}{fileName}");

            var template = new EntityRepositoryInterfaceTemplate();
            template.Name = name;
            template.TableName = SelectTable;
            template.Columns = columns;
            template.Namespace = nameSpace;
            template.IsPluralize = IsPluralize.Checked;

            var sourceManage = new SourceManage(fileName, template.TransformText());
            var sourceFile = sourceManage.Create();

            CreateFile(sourceFile, fileName, originalFile);
        }

        private void CreateFile(FileInfo sourceFile, string fileName, FileInfo originalFile)
        {
            var fileInProject = projectItems
                .GetFiles()
                .FirstOrDefault(x => x.Name == sourceFile.Name);

            if (fileInProject != null && IsReplace.Checked == false)
            {
                var outputFile = new FileInfo(Temporary.Directory + $"_{fileName}");
                outputFile.DeleteIfExisting();
                outputFile.CreateAndDispose();

                string command = $"\"{sourceFile.FullName}\" \"{originalFile.FullName}\" -o \"{outputFile.FullName}\"";
                var process = System.Diagnostics.Process.Start(temporaryRepository.Kdiff.FullName, command);
                if (process != null)
                {
                    process.WaitForExit();

                    sourceFile.DeleteIfExisting();
                    File.Copy(outputFile.FullName, sourceFile.FullName);

                    fileInProject.Delete();
                    projectItems.AddFromFileCopy(sourceFile.FullName);
                    projectItems.ContainingProject.Save();
                }
            }
            else
            {
                originalFile.DeleteIfExisting();
                projectItems.AddFromFileCopy(sourceFile.FullName);
                projectItems.ContainingProject.Save();
            }
        }

        private void OnClickImplementSave(object sender, EventArgs e)
        {
            if (ValidationButton()) return;

            UpdateConfig();

            var columns = temporaryRepository
                .GetColumns()
                .Where(x => x.TableName.Equals(SelectTable))
                .ToList();

            CreateEfRepository(columns);

            Close();
        }

        private bool ValidationButton()
        {
            if (string.IsNullOrEmpty(EntityFrameworkName.Text))
            {
                MessageBox.Show("Not found entity framework name", Text);
                return true;
            }

            if (string.IsNullOrEmpty(SelectTable))
            {
                MessageBox.Show("Select table.", Text);
                return true;
            }
            return false;
        }
    }
}
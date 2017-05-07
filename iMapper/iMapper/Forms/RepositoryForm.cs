using EnvDTE;
using iMapper.Constance.Enumeration;
using iMapper.Extensions;
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
            EntityFrameworkName.Text = temporaryRepository.EntityName ?? string.Empty;
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

        private void OnClickSave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntityFrameworkName.Text))
            {
                MessageBox.Show("Not found entity framework name", Text);
                return;
            }

            if (string.IsNullOrEmpty(SelectTable))
            {
                MessageBox.Show("Select table.", Text);
                return;
            }

            temporaryRepository.EntityName = EntityFrameworkName.Text;

            var columns = temporaryRepository
                .GetColumns()
                .Where(x => x.TableName.Equals(SelectTable))
                .ToList();

            if (IsInterfaceOnly.Checked)
            {
                CreateEfInterfaceRepository(columns);
            }

            if (IsImplementOnly.Checked)
            {
                CreateEfRepository(columns);
            }

            Close();
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
            var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
            var originalFile = new FileInfo($@"{destinationPath}{fileName}");

            var template = new EntityRepositoryTemplate();
            template.Name = name;
            template.TableName = SelectTable;
            template.Columns = columns;
            template.Namespace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
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
            var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
            var originalFile = new FileInfo($@"{destinationPath}{fileName}");

            var template = new EntityRepositoryInterfaceTemplate();
            template.Name = name;
            template.TableName = SelectTable;
            template.Columns = columns;
            template.Namespace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
            template.IsPluralize = IsPluralize.Checked;

            var sourceManage = new SourceManage(fileName, template.TransformText());
            var sourceFile = sourceManage.Create();

            CreateFile(sourceFile, fileName, originalFile);
        }

        private void CreateFile(FileInfo sourceFile, string fileName, FileInfo originalFile)
        {
            var fileInProject = projectItem.ProjectItems
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
                    projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                    projectItem.ContainingProject.Save();
                }
            }
            else
            {
                originalFile.DeleteIfExisting();
                projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                projectItem.ContainingProject.Save();
            }
        }
    }
}
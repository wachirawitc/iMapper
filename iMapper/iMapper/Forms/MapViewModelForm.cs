using EnvDTE;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class MapViewModelForm : Form
    {
        private readonly ProjectItems projectItems;
        private readonly TemporaryRepository temporaryRepository;

        public MapViewModelForm(ProjectItems projectItems)
        {
            InitializeComponent();
            this.projectItems = projectItems;

            temporaryRepository = new TemporaryRepository();
        }

        private void MapViewModelForm_Load(object sender, EventArgs e)
        {
            var tables = temporaryRepository
                .GetColumns()
                .GroupBy(x => new { x.TableSchema, x.TableName })
                .Select(x => new ComboboxItem { Text = $"[{x.Key.TableSchema}].[{x.Key.TableName}]", Value = x.Key.TableName })
                .Distinct();

            foreach (var table in tables)
            {
                Tables.Items.Add(table);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var item = Tables.SelectedItem as ComboboxItem;
            if (item != null)
            {
                var columns = temporaryRepository
                    .GetColumns()
                    .Where(x => x.TableName == (string)item.Value)
                    .ToList();

                var template = new ViewModelAspNetMvc();
                template.Namespace = "Test";
                template.Name = $"{item.Value}ViewModel";
                template.Columns = columns;

                string file = Temporary.Directory + $"{item.Value}ViewModel.cs";
                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                using (TextWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine(template.TransformText());
                    writer.Close();
                }
                projectItems.AddFromFileCopy(file);
            }

            Close();
        }

        private void Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = Tables.SelectedItem as ComboboxItem;
            if (item != null)
            {
                FileName.Text = $"{item.Value}ViewModel.cs";

                var columns = temporaryRepository
                    .GetColumns()
                    .Where(x => x.TableName == (string)item.Value)
                    .ToList();
                if (columns.Any())
                {
                    Columns.DataSource = columns;
                }
            }
        }
    }
}
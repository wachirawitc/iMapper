using EnvDTE;
using iMapper.Repository;
using System;
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
                .Select(x => $"[{x.TableSchema}].[{x.TableName}]")
                .Distinct();

            foreach (var table in tables)
            {
                Tables.Items.Add(table);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            projectItems.AddFromFileCopy(@"C:\Users\Admin\Desktop\xxx\code.txt");
            this.Close();
        }
    }
}
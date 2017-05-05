using EnvDTE;
using System;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class MapViewModelForm : Form
    {
        private readonly ProjectItems projectItems;

        public MapViewModelForm(ProjectItems projectItems)
        {
            InitializeComponent();
            this.projectItems = projectItems;
        }

        private void MapViewModelForm_Load(object sender, EventArgs e)
        {
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            projectItems.AddFromFileCopy(@"C:\Users\Admin\Desktop\xxx\code.txt");
            this.Close();
        }
    }
}
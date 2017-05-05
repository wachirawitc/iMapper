using System;
using System.Windows.Forms;
using iMapper.Repository;

namespace iMapper.Forms
{
    public partial class ConnectDatabaseForm : Form
    {
        public ConnectDatabaseForm()
        {
            InitializeComponent();
        }

        private void ConnectDatabaseForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            MsRepository repository = new MsRepository(ServerName.Text,DatabaseName.Text,Username.Text,Password.Text);
            var columns = repository.GetColumns();
            TableGrid.DataSource = columns;
        }
    }
}
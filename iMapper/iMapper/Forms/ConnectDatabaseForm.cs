using iMapper.Model;
using iMapper.Repository;
using System;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class ConnectDatabaseForm : Form
    {
        private readonly TemporaryRepository temporaryRepository;

        public ConnectDatabaseForm()
        {
            InitializeComponent();

            temporaryRepository = new TemporaryRepository();
        }

        private void ConnectDatabaseForm_Load(object sender, EventArgs e)
        {
            TableGrid.DataSource = temporaryRepository.GetColumns();

            Config config = temporaryRepository.GetConfig();
            if (config != null)
            {
                ServerName.Text = config.ServerName;
                DatabaseName.Text = config.Database;
                Username.Text = config.User;
                Password.Text = config.Password;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Config config = new Config { ServerName = ServerName.Text, Database = DatabaseName.Text, User = Username.Text, Password = Password.Text };
            MsRepository repository = new MsRepository(config.ServerName, config.Database, config.User, config.Password);

            var columns = repository.GetColumns();
            temporaryRepository.SetColumns(columns);
            temporaryRepository.SetConfig(config);

            TableGrid.DataSource = columns;
        }
    }
}
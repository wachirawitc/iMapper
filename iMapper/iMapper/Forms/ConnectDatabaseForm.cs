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
                KDiff.Text = config.KDiff;
                IsWindowsAuthentication.Checked = config.IsWindowsAuthentication;
                InitWindowsAuthentication(config.IsWindowsAuthentication);
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            new LoadForm(() =>
            {
                var config = new Config
                {
                    ServerName = ServerName.Text,
                    Database = DatabaseName.Text,
                    User = Username.Text,
                    Password = Password.Text,
                    IsWindowsAuthentication = IsWindowsAuthentication.Checked,
                    KDiff = KDiff.Text
                };
                var repository = new MsRepository(config.ServerName, config.Database, config.User, config.Password);
                if (IsWindowsAuthentication.Checked)
                {
                    repository = new MsRepository(ServerName.Text, DatabaseName.Text);
                }

                var columns = repository.GetColumns();
                temporaryRepository.SetColumns(columns);
                temporaryRepository.SetConfig(config);
                Invoke((MethodInvoker)delegate
                {
                    TableGrid.DataSource = columns;
                });
            }).ShowDialog();
        }

        private void IsWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            InitWindowsAuthentication(IsWindowsAuthentication.Checked);
        }

        private void InitWindowsAuthentication(bool isCheck)
        {
            if (isCheck)
            {
                Username.ReadOnly = true;
                Password.ReadOnly = true;
            }
            else
            {
                Username.ReadOnly = false;
                Password.ReadOnly = false;
            }
        }

        private void OnLoadKDiff(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                KDiff.Text = openFileDialog.FileName;
                var config = temporaryRepository.GetConfig();
                config.KDiff = openFileDialog.FileName;
                temporaryRepository.SetConfig(config);
            }
        }
    }
}
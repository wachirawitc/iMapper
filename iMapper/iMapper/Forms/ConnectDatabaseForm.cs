using iMapper.Model;
using iMapper.Model.Database;
using iMapper.Repository;
using iMapper.Support;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class ConnectDatabaseForm : Form
    {
        public ConnectDatabaseForm()
        {
            InitializeComponent();
            CreateFile();
        }

        private static void CreateFile()
        {
            if (Directory.Exists(Temporary.Directory) == false)
            {
                Directory.CreateDirectory(Temporary.Directory);
            }

            if (File.Exists(Temporary.TableFile) == false)
            {
                using (File.Create(Temporary.TableFile))
                {
                }
            }

            if (File.Exists(Temporary.ConfigFile) == false)
            {
                using (File.Create(Temporary.ConfigFile))
                {
                }
            }
        }

        private void ConnectDatabaseForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Temporary.TableFile))
            {
                string contents = File.ReadAllText(Temporary.TableFile);
                List<ColumnModel> columns = JsonConvert.DeserializeObject<List<ColumnModel>>(contents);
                TableGrid.DataSource = columns ?? new List<ColumnModel>();
            }

            if (File.Exists(Temporary.ConfigFile))
            {
                string contents = File.ReadAllText(Temporary.ConfigFile);
                Config config = JsonConvert.DeserializeObject<Config>(contents);
                if (config != null)
                {
                    ServerName.Text = config.ServerName;
                    DatabaseName.Text = config.Database;
                    Username.Text = config.User;
                    Password.Text = config.Password;
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Config config = new Config { ServerName = ServerName.Text, Database = DatabaseName.Text, User = Username.Text, Password = Password.Text };
            MsRepository repository = new MsRepository(config.ServerName, config.Database, config.User, config.Password);

            var columns = repository.GetColumns();
            using (TextWriter writer = new StreamWriter(Temporary.TableFile))
            {
                writer.WriteLine(JsonConvert.SerializeObject(columns));
                writer.Close();
            }

            using (TextWriter writer = new StreamWriter(Temporary.ConfigFile))
            {
                writer.WriteLine(JsonConvert.SerializeObject(config));
                writer.Close();
            }

            TableGrid.DataSource = columns;
        }
    }
}
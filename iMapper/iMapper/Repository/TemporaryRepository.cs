using iMapper.Model;
using iMapper.Model.Database;
using iMapper.Support;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace iMapper.Repository
{
    public class TemporaryRepository
    {
        public TemporaryRepository()
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

        public void SetColumns(List<ColumnModel> models)
        {
            using (TextWriter writer = new StreamWriter(Temporary.TableFile))
            {
                writer.WriteLine(JsonConvert.SerializeObject(models));
                writer.Close();
            }
        }

        public List<ColumnModel> GetColumns()
        {
            string contents = File.ReadAllText(Temporary.TableFile);
            List<ColumnModel> columns = JsonConvert.DeserializeObject<List<ColumnModel>>(contents);
            return columns ?? new List<ColumnModel>();
        }

        public void SetConfig(Config config)
        {
            using (TextWriter writer = new StreamWriter(Temporary.ConfigFile))
            {
                writer.WriteLine(JsonConvert.SerializeObject(config));
                writer.Close();
            }
        }

        public Config GetConfig()
        {
            string contents = File.ReadAllText(Temporary.ConfigFile);
            return JsonConvert.DeserializeObject<Config>(contents);
        }
    }
}
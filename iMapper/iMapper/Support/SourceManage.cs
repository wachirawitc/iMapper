using System.IO;

namespace iMapper.Support
{
    public class SourceManage
    {
        private readonly string name;
        private readonly string content;

        public SourceManage(string name, string content)
        {
            this.name = name;
            this.content = content;
        }

        public FileInfo Create()
        {
            string file = Temporary.Directory + name;
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using (TextWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(content);
                writer.Close();
            }

            return new FileInfo(file);
        }
    }
}
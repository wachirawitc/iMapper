using System;

namespace iMapper.Support
{
    public static class Temporary
    {
        public static string Directory => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\iMapper\";

        public static string TableFile => Directory + "Table.json";

        public static string ConfigFile => Directory + "Config.json";
    }
}
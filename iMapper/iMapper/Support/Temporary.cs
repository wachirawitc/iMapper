namespace iMapper.Support
{
    public static class Temporary
    {
        public static string SolutionPath { get; set; }

        public static string Directory => SolutionPath + @"\.iMapper\";

        public static string TableFile => Directory + "Table.json";

        public static string ConfigFile => Directory + "Config.json";
    }
}
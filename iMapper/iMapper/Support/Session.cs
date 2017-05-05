using System.Collections.Generic;
using System.IO;

namespace iMapper.Support
{
    public static class Session
    {
        public static List<DirectoryInfo> GetMapDirectories => new List<DirectoryInfo>
        {
            Directory.GetParent(@"c:\users\admin\documents\visual studio 2015\Projects\ConsoleApplication3\ConsoleApplication3\ViewModel\"),
            Directory.GetParent(@"C:\Users\Admin\Documents\Visual Studio 2015\Projects\ConsoleApplication3\Model\")
        };
    }
}
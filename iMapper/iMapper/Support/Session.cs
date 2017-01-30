using System.Collections.Generic;
using System.IO;

namespace iMapper.Support
{
    public static class Session
    {
        public static List<DirectoryInfo> GetMapDirectories
        {
            get
            {
                var model = new List<DirectoryInfo>
                {
                    Directory.GetParent(@"D:\Project\HRMS\HRMS\HRMS\ViewModels\"),
                    Directory.GetParent(@"D:\Project\HRMS\HRMS\HR.Models\")
                };
                return model;
            }
        }
    }
}
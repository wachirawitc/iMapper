using System;
using System.IO;

namespace iMapper.Extensions
{
    public static class FileExtension
    {
        public static void DeleteIfExisting(this FileInfo fileInfo)
        {
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }

        public static void CreateAndDispose(this FileInfo fileInfo)
        {
            using (File.Create(fileInfo.FullName)) { }
        }

        public static DateTime GetLastWriteTime(this FileInfo fileInfo)
        {
            return File.GetLastWriteTime(fileInfo.FullName);
        }
    }
}
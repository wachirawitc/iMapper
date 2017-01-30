using EnvDTE;
using System.IO;

namespace iMapper.Extensions
{
    public static class ProjectExtension
    {
        public static DirectoryInfo GetDirectoryInfo(this Project project)
        {
            var fullPathProperty = project?.Properties?.Item("FullPath");
            if (fullPathProperty != null)
            {
                string fullPath = fullPathProperty.Value.ToString();
                if (string.IsNullOrEmpty(fullPath) == false && Directory.Exists(fullPath))
                {
                    return Directory.GetParent(fullPath);
                }
            }
            return null;
        }
    }
}
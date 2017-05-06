using EnvDTE;
using System.IO;

namespace iMapper.Extensions
{
    public static class ProjectExtension
    {
        public static DirectoryInfo GetFullPathProperty(this Project project)
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

        public static string GetDefaultNamespaceProperty(this Project project)
        {
            var fullPathProperty = project?.Properties?.Item("DefaultNamespace");
            return fullPathProperty?.Value.ToString();
        }
    }
}
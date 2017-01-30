using EnvDTE;
using System.IO;

namespace iMapper.Extensions
{
    public static class ProjectItemExtension
    {
        public static bool IsFolder(this ProjectItem projectItem)
        {
            if (projectItem == null)
            {
                return false;
            }

            const string physicalFolder = "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";
            return projectItem.Kind == physicalFolder;
        }

        public static FileInfo GetFileInfo(this ProjectItem projectItem)
        {
            var fullPathProperty = projectItem?.Properties?.Item("FullPath");
            if (fullPathProperty != null)
            {
                string fullPath = fullPathProperty.Value.ToString();
                if (File.Exists(fullPath))
                {
                    return new FileInfo(fullPath);
                }
            }
            return null;
        }
    }
}
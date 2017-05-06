using EnvDTE;
using iMapper.Constance;
using iMapper.Exception;
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

            return projectItem.Kind == KindElement.PhysicalFolder;
        }

        public static FileInfo GetFullPathProperty(this ProjectItem projectItem)
        {
            if (projectItem.Kind != KindElement.PhysicalFile)
            {
                throw new ProjectItemException("Project item are not file.");
            }

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

        public static DirectoryInfo GetFullPathDirectoryProperty(this ProjectItem projectItem)
        {
            if (IsFolder(projectItem) == false)
            {
                throw new ProjectItemException("Project item are not folder.");
            }

            var fullPathProperty = projectItem?.Properties?.Item("FullPath");
            if (fullPathProperty != null)
            {
                string fullPath = fullPathProperty.Value.ToString();
                if (Directory.Exists(fullPath))
                {
                    return new DirectoryInfo(fullPath);
                }
            }
            return null;
        }
    }
}
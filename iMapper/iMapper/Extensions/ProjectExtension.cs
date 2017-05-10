using System.Collections.Generic;
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

        private static IEnumerable<Project> GetSolutionFolderProjects(Project project)
        {
            var projects = new List<Project>();
            var y = project.ProjectItems.Count;
            for (var i = 1; i <= y; i++)
            {
                var x = project.ProjectItems.Item(i).SubProject;
                var subProject = x as Project;
                if (subProject != null)
                {
                    
                }
            }

            return projects;
        }
    }
}
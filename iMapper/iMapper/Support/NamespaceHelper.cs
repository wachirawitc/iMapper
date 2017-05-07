using EnvDTE;
using iMapper.Constance;
using iMapper.Extensions;
using System.IO;
using System.Linq;

namespace iMapper.Support
{
    public static class NamespaceHelper
    {
        public static string Get(Project project, ProjectItem projectItem)
        {
            const string defaultNamespace = "DefaultNamespace";
            var projctNamespace = project.GetDefaultNamespaceProperty();

            if (projectItem.Kind != KindElement.PhysicalFolder)
            {
                return projctNamespace ?? defaultNamespace;
            }

            var projectPath = project.GetFullPathProperty();
            var folderPath = projectItem.GetFullPathDirectoryProperty();

            if (projectPath == null || folderPath == null)
            {
                return defaultNamespace;
            }
            return GetNamespace(projectPath, folderPath, projctNamespace);
        }

        public static string Get(Project project)
        {
            const string defaultNamespace = "DefaultNamespace";
            var projctNamespace = project.GetDefaultNamespaceProperty();
            return projctNamespace ?? defaultNamespace;
        }

        public static string GetNamespace(DirectoryInfo project, DirectoryInfo subProject, string solutionNamespace)
        {
            if (project == null || subProject == null)
            {
                return solutionNamespace;
            }

            var subPath = subProject.FullName.Replace(project.FullName, string.Empty);
            var subFolder = subPath.Split('\\')
                .ToList()
                .Where(x => string.IsNullOrEmpty(x) == false)
                .Select(x => x.Trim());

            var targetNamespace = string.Join(".", subFolder);
            return $"{solutionNamespace}.{targetNamespace}";
        }
    }
}
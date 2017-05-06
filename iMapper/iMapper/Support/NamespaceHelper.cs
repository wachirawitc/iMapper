using System.IO;
using System.Linq;

namespace iMapper.Support
{
    public static class NamespaceHelper
    {
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
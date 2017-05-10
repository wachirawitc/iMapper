using EnvDTE;
using EnvDTE80;
using iMapper.Constance;
using iMapper.Model;
using System.Collections.Generic;
using System.Linq;

namespace iMapper.Extensions
{
    public static class Dte2Extension
    {
        public static List<ClassModel> GetClass(this DTE2 dte2)
        {
            var classElements = new List<ClassModel>();
            var projects = dte2.Solution.Projects.Cast<Project>();
            foreach (var project in projects)
            {
                var projectItems = project.ProjectItems.GetFilesIncludeSubFolder().ToList();
                var fileCodeModel2S = projectItems
                    .Where(x => x.Kind == KindElement.PhysicalFile)
                    .Where(x => x.FileCodeModel is FileCodeModel2)
                    .Select(x => x.FileCodeModel as FileCodeModel2)
                    .ToList();
                if (fileCodeModel2S.Any())
                {
                    foreach (var fileCode in fileCodeModel2S)
                    {
                        var codeElements = fileCode.CodeElements;
                        foreach (CodeElement codeElement in codeElements)
                        {
                            if (codeElement.Kind == vsCMElement.vsCMElementNamespace)
                            {
                                classElements.AddRange(codeElement.Children.Cast<CodeElement>()
                                    .Select(x => x.GetClassElement())
                                    .Where(classElement => classElement != null));
                            }
                        }
                    }
                }
            }

            return classElements;
        }
    }
}
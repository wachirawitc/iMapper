using EnvDTE;
using EnvDTE80;
using iMapper.Model;
using System.Collections.Generic;
using System.Linq;

namespace iMapper.Extensions
{
    public static class ProjectItemsExtension
    {
        public static IEnumerable<ProjectItem> GetFilesIncludeSubFolder(this ProjectItems projectItems)
        {
            if (projectItems != null)
            {
                foreach (ProjectItem item in projectItems)
                {
                    yield return item;

                    if (item.SubProject != null)
                    {
                        foreach (var childItem in GetFilesIncludeSubFolder(item.SubProject.ProjectItems))
                            yield return childItem;
                    }
                    else
                    {
                        foreach (ProjectItem childItem in GetFilesIncludeSubFolder(item.ProjectItems))
                            yield return childItem;
                    }
                }
            }
        }

        public static IEnumerable<ProjectItem> GetFiles(this ProjectItems projectItems)
        {
            if (projectItems != null)
            {
                foreach (ProjectItem item in projectItems)
                {
                    yield return item;

                    if (item.SubProject == null)
                    {
                        foreach (ProjectItem childItem in GetFiles(item.ProjectItems))
                            yield return childItem;
                    }
                }
            }
        }

        public static IEnumerable<CodeClassModel> GetCodeClasses(this ProjectItems projectItems)
        {
            var models = new List<CodeClassModel>();
            
            var items = GetFilesIncludeSubFolder(projectItems);
            foreach (var item in items)
            {
                string name = item.Name;
                var fileCode = item.FileCodeModel as FileCodeModel2;
                if (fileCode != null)
                {
                    var codeModel = new CodeClassModel();
                    codeModel.ProjectItem = item;

                    var codeElements = fileCode.CodeElements;
                    foreach (CodeElement codeElement in codeElements)
                    {
                        if (codeElement.Kind == vsCMElement.vsCMElementNamespace)
                        {
                            foreach (CodeElement element in codeElement.Children)
                            {
                                if (element.Kind == vsCMElement.vsCMElementClass)
                                {
                                    var codeClass = element as CodeClass;
                                    if (codeClass != null)
                                    {
                                        codeModel.CodeClasses.Add(codeClass);
                                    }
                                }
                            }
                        }
                    }

                    models.Add(codeModel);
                }
            }

            return models;
        }
    }
}
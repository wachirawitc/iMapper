using EnvDTE;
using iMapper.Exception;
using System.Collections.Generic;
using System.IO;

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
    }
}
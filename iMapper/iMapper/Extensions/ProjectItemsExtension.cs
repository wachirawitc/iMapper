using EnvDTE;
using System.Collections.Generic;

namespace iMapper.Extensions
{
    public static class ProjectItemsExtension
    {
        public static IEnumerable<ProjectItem> GetFiles(this ProjectItems projectItems)
        {
            if (projectItems != null)
            {
                foreach (ProjectItem item in projectItems)
                {
                    yield return item;

                    if (item.SubProject != null)
                    {
                        foreach (var childItem in GetFiles(item.SubProject.ProjectItems))
                            yield return childItem;
                    }
                    else
                    {
                        foreach (ProjectItem childItem in GetFiles(item.ProjectItems))
                            yield return childItem;
                    }
                }
            }
        }
    }
}
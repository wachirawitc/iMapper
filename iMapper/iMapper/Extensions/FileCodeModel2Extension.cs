using EnvDTE;
using EnvDTE80;
using iMapper.Model;
using System.Collections.Generic;
using System.Linq;

namespace iMapper.Extensions
{
    public static class FileCodeModel2Extension
    {
        public static List<ClassModel> GetClass(this FileCodeModel2 fileCode)
        {
            if (fileCode == null)
            {
                return new List<ClassModel>();
            }

            var classElements = new List<ClassModel>();
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
            return classElements;
        }
    }
}
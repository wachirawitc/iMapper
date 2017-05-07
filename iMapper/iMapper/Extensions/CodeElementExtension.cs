using EnvDTE;
using iMapper.Model;
using System.Collections.Generic;
using System.Linq;

namespace iMapper.Extensions
{
    public static class CodeElementExtension
    {
        public static ClassModel GetClassElement(this CodeElement element)
        {
            try
            {
                var model = new ClassModel();
                model.Name = element.Name;
                model.FullName = element.FullName;

                var elementClass = element.Kind;
                if (elementClass == vsCMElement.vsCMElementClass)
                {
                    model.Members = GetMembers(element);
                    if (model.Members.Any() == false)
                    {
                        return null;
                    }
                }

                return model;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private static List<PropertyModel> GetMembers(CodeElement element)
        {
            var model = new List<PropertyModel>();

            var elementClass = element.Kind;
            if (elementClass == vsCMElement.vsCMElementClass)
            {
                foreach (CodeElement child in element.Children)
                {
                    if (child.Kind == vsCMElement.vsCMElementProperty)
                    {
                        var property = (CodeProperty)child;

                        model.Add(new PropertyModel
                        {
                            Name = child.Name,
                            TypeFullName = property.Type.AsFullName
                        });
                    }
                }
            }

            return model;
        }
    }
}
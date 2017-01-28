using iMapper.Model;
using System.Linq;
using System.Text;

namespace iMapper.Template.ModelTemplate
{
    public class MapperModelTemplate
    {
        private readonly StringBuilder mapping;
        private readonly ClassElement source;
        private readonly ClassElement destination;

        public MapperModelTemplate(ClassElement source, ClassElement destination)
        {
            mapping = new StringBuilder();
            this.source = source;
            this.destination = destination;
        }

        public void Verify()
        {
            if (IsNull())
            {
                return;
            }

            mapping.AppendLine($"{destination.FullName} model = null;");
            mapping.AppendLine("if(source != null)");
            mapping.AppendLine("{");

            if (destination.Members.Any())
            {
                mapping.AppendLine($"model = new {destination.FullName}();");

                foreach (var member in destination.Members)
                {
                    var name = member.Element.Name;
                    var type = member.Type.AsFullName;

                    var element = source.Members.FirstOrDefault(x => x.Element.Name.Equals(name) && x.Type.AsFullName.Equals(type));

                    if (element != null)
                    {
                        mapping.AppendLine($"model.{name} = source.{name};");
                    }
                }
            }

            mapping.AppendLine("}");
            mapping.AppendLine("return model;");
        }

        public string GetText()
        {
            return mapping.ToString();
        }

        private bool IsNull()
        {
            return destination == null ||
                   source == null ||
                   destination.Members == null ||
                   destination.Members.Any() == false ||
                   source.Members == null ||
                   source.Members.Any() == false;
        }
    }
}
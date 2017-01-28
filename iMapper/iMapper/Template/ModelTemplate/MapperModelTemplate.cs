using iMapper.Model;
using System.Collections.Generic;
using System.Linq;

namespace iMapper.Template.ModelTemplate
{
    public class MapperModelTemplate
    {
        private readonly ClassElement source;
        private readonly ClassElement destination;

        public MapperModelTemplate(ClassElement source, ClassElement destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public string GetText()
        {
            var template = new MapperTemplate { Session = new Dictionary<string, object>() };
            template.Session = new Dictionary<string, object>
            {
                { "source", source },
                { "destination", destination }
            };
            template.Initialize();
            return template.TransformText();
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
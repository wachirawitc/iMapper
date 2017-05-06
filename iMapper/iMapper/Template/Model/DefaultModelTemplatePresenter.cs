using Humanizer;
using iMapper.Model.Database;
using System.Collections.Generic;

namespace iMapper.Template.Model
{
    public partial class DefaultModelTemplate
    {
        public bool IsPascalize { get; set; }

        public string Name { get; set; }

        public string Namespace { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public string GetName(string name)
        {
            if (IsPascalize)
            {
                return name.Pascalize();
            }
            return name;
        }
    }
}
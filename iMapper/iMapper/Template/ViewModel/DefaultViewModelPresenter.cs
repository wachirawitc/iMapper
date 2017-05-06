using iMapper.Model.Database;
using System.Collections.Generic;

namespace iMapper.Template.ViewModel
{
    public partial class DefaultViewModel
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public List<ColumnModel> Columns { get; set; }
    }
}
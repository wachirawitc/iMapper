using System.Collections.Generic;

namespace iMapper.Model.Database
{
    public class TableModel
    {
        public string Name { get; set; }
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public List<ColumnModel> Columns { get; set; }

        public TableModel()
        {
            Columns = new List<ColumnModel>();
        }
    }
}
using System.Collections.Generic;

namespace iMapper.Model.Database
{
    public class StoredProceduresModel
    {
        public string Name { get; set; }

        public List<StoredProceduresParameterModel> Parameters { get; set; }

        public List<StoredProceduresColumnsModel> Columns { get; set; }
        public string SchemaName { get; set; }

        public StoredProceduresModel()
        {
            Parameters = new List<StoredProceduresParameterModel>();
            Columns = new List<StoredProceduresColumnsModel>();
        }
    }
}
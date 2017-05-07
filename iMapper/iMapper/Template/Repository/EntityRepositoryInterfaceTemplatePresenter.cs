using Humanizer;
using iMapper.Extensions;
using iMapper.Model.Database;
using System.Collections.Generic;
using System.Linq;

namespace iMapper.Template.Repository
{
    public partial class EntityRepositoryInterfaceTemplate
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        private string tableName;

        public string TableName
        {
            get { return IsPluralize ? tableName.Pluralize() : tableName; }
            set { tableName = value; }
        }

        public string TableNamePascalize => TableName.Pascalize();

        public string TableNameCamelize => TableName.Camelize();

        public string TableNamePluralize => TableName.Pluralize();

        public List<ColumnModel> Columns { get; set; }

        public string GetPkParameter()
        {
            var items = Columns.Where(x => x.IsPrimaryKey)
                .Select(x => $"{x.DataType.GetMsType(x.IsNullable)} {x.ColumnName.Camelize()}")
                .ToList();
            return string.Join(",", items);
        }

        public bool HasPk => Columns.Any(x => x.IsPrimaryKey);

        public bool IsPluralize { get; set; }
    }
}
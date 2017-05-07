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

        public string TableName { get; set; }

        public string EfTableName => IsPluralize ? TableName.Pluralize() : TableName;

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
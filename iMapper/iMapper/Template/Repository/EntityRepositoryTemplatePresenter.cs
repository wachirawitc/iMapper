using Humanizer;
using iMapper.Extensions;
using iMapper.Model.Database;
using System.Collections.Generic;
using System.Linq;

namespace iMapper.Template.Repository
{
    public partial class EntityRepositoryTemplate
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public string EntityName { get; set; }

        public string TableName { get; set; }

        public string EfTableName => IsPluralize ? TableName.Pluralize() : TableName;

        public string TableNamePascalize => TableName.Pascalize();

        public string TableNameCamelize => TableName.Camelize();

        public string TableNamePluralize => TableName.Pluralize();

        public List<ColumnModel> Columns { get; set; }

        public List<ColumnModel> ColumnsWithoutPk => Columns.Where(x => x.IsPrimaryKey == false).ToList();

        public string GetPkParameter()
        {
            var items = Columns.Where(x => x.IsPrimaryKey)
                .Select(x => $"{x.DataType.GetMsType(x.IsNullable)} {x.ColumnName.Camelize()}")
                .ToList();
            return string.Join(",", items);
        }

        public string GetPkArgument()
        {
            var items = Columns.Where(x => x.IsPrimaryKey)
                .Select(x => $"x.{x.ColumnName} == {x.ColumnName.Camelize()}")
                .ToList();
            return string.Join(",", items);
        }

        public string GetPkUpdateArgument()
        {
            var items = Columns.Where(x => x.IsPrimaryKey)
                .Select(x => $"x.{x.ColumnName} == {TableNameCamelize}.{x.ColumnName}")
                .ToList();
            return string.Join(",", items);
        }

        public bool HasPk => Columns.Any(x => x.IsPrimaryKey);

        public bool IsPluralize { get; set; }
    }
}
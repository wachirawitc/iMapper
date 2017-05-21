using iMapper.Model.Database;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using iMapper.Extensions;

namespace iMapper.Template.Service
{
    public partial class DefaultServiceTemplate
    {
        public string Name { get; set; }

        public string TableName { get; set; }

        public string TableNameCamelize => TableName.Camelize();

        public string TableNamePluralize => TableName.Pluralize();

        public string Namespace { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public List<ColumnModel> ColumnsWithoutPk => Columns.Where(x => x.IsPrimaryKey == false).ToList();

        public string GetPkParameter()
        {
            var items = Columns.Where(x => x.IsPrimaryKey)
                .Select(x => $"{x.DataType.GetMsType(x.IsNullable)} {x.ColumnName.Camelize()}")
                .ToList();
            return string.Join(" , ", items);
        }

        public bool HasPk => Columns.Any(x => x.IsPrimaryKey);
    }
}
using Humanizer;
using iMapper.Extensions;
using iMapper.Model.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iMapper.Template.Validation
{
    public partial class Custom1ValidationTemplate
    {
        public bool IsPascalize { get; set; }

        public string Name { get; set; }

        public string ValidatorName { get; set; }

        public string Namespace { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public string GetName(string name)
        {
            return IsPascalize ? name.Pascalize() : name;
        }

        public string GetRepositoryVariable()
        {
            var builder = new StringBuilder();

            foreach (var table in Columns.Where(x => x.IsRelation && string.IsNullOrEmpty(x.RelationTable) == false).Select(x => x.RelationTable).Distinct())
            {
                builder.AppendLine($"\t\tprivate readonly I{table.Pascalize()}Repository {table.Camelize()}Repository;");
            }

            return builder.ToString();
        }

        public string GetRepositoryInitVariable()
        {
            var builder = new StringBuilder();

            foreach (var table in Columns.Where(x => x.IsRelation && string.IsNullOrEmpty(x.RelationTable) == false).Select(x => x.RelationTable).Distinct())
            {
                builder.AppendLine($"\t\t\tthis.{table.Camelize()}Repository = {table.Camelize()}Repository;");
            }

            return builder.ToString();
        }

        public string GetRepositoryParameter()
        {
            var builder = new List<string>();

            foreach (var table in Columns
                .Where(x => x.IsRelation && string.IsNullOrEmpty(x.RelationTable) == false)
                .Select(x => x.RelationTable)
                .Distinct())
            {
                builder.Add($"\t\t\tI{table.Pascalize()}Repository {table.Camelize()}Repository");
            }

            return string.Join(",\n", builder);
        }

        private static bool IsString(ColumnModel column)
        {
            return "string".Equals(column.DataType.GetMsType(column.IsNullable));
        }

        public string GetStringRule(ColumnModel column)
        {
            if (IsString(column) == false)
            {
                return string.Empty;
            }

            string name = GetName(column.ColumnName);

            var rules = new List<string>();

            if (column.IsNullable == false)
            {
                rules.Add($"\t\t\tAdd(new Required(Model.{name}, Text.{name}));");
            }

            if (column.CharacterMaximumLength != null)
            {
                rules.Add($"\t\t\tAdd(new StringLength(Model.{name}, {column.CharacterMaximumLength.Value}, Text.{name}));");
            }

            if (column.IsRelation && string.IsNullOrEmpty(column.RelationTable) == false)
            {
                var relationTable = column.RelationTable;
                rules.Add($"\t\t\tAdd(() => {relationTable.Camelize()}Repository.IsExisting(Model.{name}), ValidationMessage.NotFound{relationTable});");
            }

            return string.Join("\n", rules);
        }

        private static bool IsNumber(ColumnModel column)
        {
            var result = column.DataType.GetMsType(column.IsNullable);
            var targets = new List<string> { "int", "short", "long", "int?", "short?", "long?" };
            return targets.Contains(result);
        }

        public string GetNumberRule(ColumnModel column)
        {
            if (IsNumber(column) == false)
            {
                return string.Empty;
            }

            string name = GetName(column.ColumnName);
            var rules = new List<string>();

            var relationTable = column.RelationTable;
            if (column.IsNullable && column.IsRelation && string.IsNullOrEmpty(column.RelationTable) == false)
            {
                rules.Add($"\t\t\tAdd(() => Model.{name} != null && {relationTable.Camelize()}Repository.IsExisting(Model.{name}.Value), ValidationMessage.NotFound{relationTable});");
            }
            else if (column.IsRelation && string.IsNullOrEmpty(column.RelationTable) == false)
            {
                rules.Add($"\t\t\tAdd(() => {relationTable.Camelize()}Repository.IsExisting(Model.{name}), ValidationMessage.NotFound{relationTable});");
            }

            return string.Join("\n", rules);
        }

        private static bool IsDateTime(ColumnModel column)
        {
            var result = column.DataType.GetMsType(column.IsNullable);
            var targets = new List<string> { "DateTime", "DateTime?" };
            return targets.Contains(result);
        }

        private string GetDateTimeValidations(ColumnModel column)
        {
            if (IsDateTime(column) == false)
            {
                return string.Empty;
            }

            string name = GetName(column.ColumnName);
            var rules = new List<string>();

            if (column.IsNullable == false)
            {
                rules.Add($"\t\t\tAdd(new Required(Model.{name}, Text.{name}));");
            }

            if (column.IsRelation && string.IsNullOrEmpty(column.RelationTable) == false)
            {
                var relationTable = column.RelationTable;
                rules.Add($"\t\t\tAdd(() => {relationTable.Camelize()}Repository.IsExisting(Model.{name}), ValidationMessage.NotFound{relationTable});");
            }

            return string.Join("\n", rules);
        }
    }
}
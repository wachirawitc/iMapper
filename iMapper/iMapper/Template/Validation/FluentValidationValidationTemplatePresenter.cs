using Humanizer;
using iMapper.Extensions;
using iMapper.Model.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iMapper.Template.Validation
{
    public partial class FluentValidationValidationTemplate
    {
        public bool IsPascalize { get; set; }

        public string Name { get; set; }

        public string ValidatorName { get; set; }

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
            rules.Add($@"RuleFor(x => x.{name})");
            rules.Add("\t.Cascade(CascadeMode.StopOnFirstFailure)");

            if (column.IsNullable == false)
            {
                rules.Add("\t\t.NotNull().WithLocalizedMessage(() => ValidationMessage.Require)");
                rules.Add("\t\t.NotEmpty().WithLocalizedMessage(() => ValidationMessage.Require)");
            }

            if (column.CharacterMaximumLength != null)
            {
                rules.Add($"\t\t.Length(0, {column.CharacterMaximumLength.Value}).WithLocalizedMessage(() => ValidationMessage.InvalidStringLength)");
            }

            if (column.IsRelation && string.IsNullOrEmpty(column.RelationTable) == false)
            {
                var relationTable = column.RelationTable;
                var columnName = column.ColumnName;
                rules.Add($"\t\t.Must({columnName.Camelize()} => {relationTable.Camelize()}Repository.IsExisting({columnName.Camelize()})).WithMessage(ValidationMessage.NotExist)");
            }

            return string.Join("\n", rules) + ";";
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
            rules.Add($@"RuleFor(x => x.{name})");
            rules.Add("\t.Cascade(CascadeMode.StopOnFirstFailure)");

            if (column.IsNullable == false)
            {
                rules.Add("\t\t.NotNull().WithLocalizedMessage(() => ValidationMessage.Require)");
            }

            if (column.IsRelation && string.IsNullOrEmpty(column.RelationTable) == false)
            {
                var relationTable = column.RelationTable;
                var columnName = column.ColumnName;
                rules.Add($"\t\t.Must({columnName.Camelize()} => {relationTable.Camelize()}Repository.IsExisting({columnName.Camelize()})).WithMessage(ValidationMessage.NotExist)");
            }

            return string.Join("\n", rules) + ";";
        }

        public string GetRepositoryVariable()
        {
            var builder = new StringBuilder();

            foreach (var table in Columns.Where(x => x.IsRelation && string.IsNullOrEmpty(x.RelationTable) == false).Select(x => x.RelationTable).Distinct())
            {
                builder.AppendLine($@"private readonly I{table.Pascalize()}Repository {table.Camelize()}Repository;");
            }

            return builder.ToString();
        }

        public string GetRepositoryInitVariable()
        {
            var builder = new StringBuilder();

            foreach (var table in Columns.Where(x => x.IsRelation && string.IsNullOrEmpty(x.RelationTable) == false).Select(x => x.RelationTable).Distinct())
            {
                builder.AppendLine($@"{table.Camelize()}Repository = new {table.Pascalize()}Repository();");
            }

            return builder.ToString();
        }
    }
}
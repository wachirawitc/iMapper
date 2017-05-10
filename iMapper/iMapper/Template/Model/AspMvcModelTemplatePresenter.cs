using Humanizer;
using iMapper.Extensions;
using iMapper.Model.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace iMapper.Template.Model
{
    public partial class AspMvcModelTemplate
    {
        public bool IsPascalize { get; set; }

        public string Name { get; set; }

        public string Namespace { get; set; }

        public List<ColumnModel> Columns { get; set; }
        public string ResXResourceName { get; set; }

        public string GetName(string name)
        {
            if (IsPascalize)
            {
                return name.Pascalize();
            }
            return name;
        }

        public string GetAttribute(ColumnModel column)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var validation in validations)
            {
                builder.AppendFormat(validation(column));
            }
            return builder.ToString();
        }

        private List<Func<ColumnModel, string>> validations;

        public AspMvcModelTemplate()
        {
            validations = new List<Func<ColumnModel, string>>
            {
                column =>  IsString(column) ? GetStringValidations(column)  : string.Empty,
                column =>  IsNumber(column) ? GetNumberValidations(column)  : string.Empty,
                column =>  IsDateTime(column) ? GetDateTimeValidations(column)  : string.Empty
            };
        }

        #region String

        private static bool IsString(ColumnModel column)
        {
            return "string".Equals(column.DataType.GetMsType(column.IsNullable));
        }

        private string GetStringValidations(ColumnModel column)
        {
            string columnName = GetName(column.ColumnName);

            var builder = new StringBuilder();
            builder.AppendLine($"\t\t[Display(Name = nameof({columnName}), ResourceType = typeof({ResXResourceName}))]");

            if (column.IsNullable == false)
            {
                builder.AppendLine("\t\t[Required]");
            }

            if (column.CharacterMaximumLength != null)
            {
                builder.AppendLine($"\t\t[StringLength({column.CharacterMaximumLength.Value})]");
            }

            return builder.ToString();
        }

        #endregion String

        #region Number

        private static bool IsNumber(ColumnModel column)
        {
            var result = column.DataType.GetMsType(column.IsNullable);
            var targets = new List<string> { "int", "short", "long", "int?", "short?", "long?" };
            return targets.Contains(result);
        }

        private string GetNumberValidations(ColumnModel column)
        {
            string columnName = GetName(column.ColumnName);

            var builder = new StringBuilder();
            builder.AppendLine($"\t\t[Display(Name = nameof({columnName}), ResourceType = typeof({ResXResourceName}))]");

            if (column.IsNullable == false)
            {
                builder.AppendLine("\t\t[Required]");
            }

            if (column.CharacterMaximumLength != null)
            {
                builder.AppendLine($"\t\t[Range(0, {column.CharacterMaximumLength.Value})]");
            }

            return builder.ToString();
        }

        #endregion Number

        #region DateTime

        private static bool IsDateTime(ColumnModel column)
        {
            var result = column.DataType.GetMsType(column.IsNullable);
            var targets = new List<string> { "DateTime", "DateTime?" };
            return targets.Contains(result);
        }

        private string GetDateTimeValidations(ColumnModel column)
        {
            string columnName = GetName(column.ColumnName);

            var builder = new StringBuilder();
            builder.AppendLine($"\t\t[Display(Name = nameof({columnName}), ResourceType = typeof({ResXResourceName}))]");

            if (column.IsNullable == false)
            {
                builder.AppendLine("\t\t[Required]");
            }

            builder.AppendLine("\t\t[DataType(DataType.Date)]");
            return builder.ToString();
        }

        #endregion DateTime
    }
}
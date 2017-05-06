using System;
using System.Collections.Generic;

namespace iMapper.Support.DataType
{
    public class SharpType
    {
        private const string DefaultType = null;

        public List<Func<string, bool, string>> Types;

        public SharpType()
        {
            Types = new List<Func<string, bool, string>>
            {
                (databaseType, isNull) =>
                    "bigint".Equals(databaseType) && isNull == false
                        ? "long"
                        : "bigint".Equals(databaseType) && isNull ? "long?" : DefaultType,

                (databaseType, isNull) =>
                    "binary".Equals(databaseType) && isNull == false
                        ? "byte[]"
                        : "binary".Equals(databaseType) && isNull ? "byte[]" : DefaultType,

                (databaseType, isNull) =>
                    "char".Equals(databaseType) && isNull == false
                        ? "string"
                        : "char".Equals(databaseType) && isNull ? "string" : DefaultType,

                (databaseType, isNull) =>
                    "date".Equals(databaseType) && isNull == false
                        ? "DateTime"
                        : "date".Equals(databaseType) && isNull ? "DateTime?" : DefaultType,

                (databaseType, isNull) =>
                    "datetime".Equals(databaseType) && isNull == false
                        ? "DateTime"
                        : "datetime".Equals(databaseType) && isNull ? "DateTime?" : DefaultType,

                (databaseType, isNull) =>
                    "datetime2".Equals(databaseType) && isNull == false
                        ? "DateTime"
                        : "datetime2".Equals(databaseType) && isNull ? "DateTime?" : DefaultType,

                (databaseType, isNull) =>
                    "datetimeoffset".Equals(databaseType) && isNull == false
                        ? "DateTimeOffset"
                        : "datetimeoffset".Equals(databaseType) && isNull ? "DateTimeOffset?" : DefaultType,

                (databaseType, isNull) =>
                    "decimal".Equals(databaseType) && isNull == false
                        ? "decimal"
                        : "decimal".Equals(databaseType) && isNull ? "decimal?" : DefaultType,

                (databaseType, isNull) =>
                    "float".Equals(databaseType) && isNull == false
                        ? "double"
                        : "float".Equals(databaseType) && isNull ? "double?" : DefaultType,

                (databaseType, isNull) =>
                    "image".Equals(databaseType) && isNull == false
                        ? "byte[]"
                        : "image".Equals(databaseType) && isNull ? "byte[]" : DefaultType,

                (databaseType, isNull) =>
                    "int".Equals(databaseType) && isNull == false
                        ? "int"
                        : "int".Equals(databaseType) && isNull ? "int?" : DefaultType,

                (databaseType, isNull) =>
                    "money".Equals(databaseType) && isNull == false
                        ? "decimal"
                        : "money".Equals(databaseType) && isNull ? "decimal?" : DefaultType,

                (databaseType, isNull) =>
                    "nchar".Equals(databaseType) && isNull == false
                        ? "string"
                        : "nchar".Equals(databaseType) && isNull ? "string" : DefaultType,

                (databaseType, isNull) =>
                    "ntext".Equals(databaseType) && isNull == false
                        ? "string"
                        : "ntext".Equals(databaseType) && isNull ? "string" : DefaultType,

                (databaseType, isNull) =>
                    "numeric".Equals(databaseType) && isNull == false
                        ? "decimal"
                        : "numeric".Equals(databaseType) && isNull ? "decimal?" : DefaultType,

                (databaseType, isNull) =>
                    "nvarchar".Equals(databaseType) && isNull == false
                        ? "string"
                        : "nvarchar".Equals(databaseType) && isNull ? "string" : DefaultType,

                (databaseType, isNull) =>
                    "rowversion".Equals(databaseType) && isNull == false
                        ? "byte[]"
                        : "rowversion".Equals(databaseType) && isNull ? "byte[]" : DefaultType,

                (databaseType, isNull) =>
                    "smalldatetime".Equals(databaseType) && isNull == false
                        ? "DateTime"
                        : "smalldatetime".Equals(databaseType) && isNull ? "DateTime?" : DefaultType,

                (databaseType, isNull) =>
                    "smallint".Equals(databaseType) && isNull == false
                        ? "short"
                        : "smallint".Equals(databaseType) && isNull ? "short?" : DefaultType,

                (databaseType, isNull) =>
                    "smallmoney".Equals(databaseType) && isNull == false
                        ? "decimal"
                        : "smallmoney".Equals(databaseType) && isNull ? "decimal?" : DefaultType,

                (databaseType, isNull) =>
                    "text".Equals(databaseType) && isNull == false
                        ? "string"
                        : "text".Equals(databaseType) && isNull ? "string" : DefaultType,

                (databaseType, isNull) =>
                    "time".Equals(databaseType) && isNull == false
                        ? "TimeSpan"
                        : "time".Equals(databaseType) && isNull ? "TimeSpan?" : DefaultType,

                (databaseType, isNull) =>
                    "timestamp".Equals(databaseType) && isNull == false
                        ? "byte[]"
                        : "timestamp".Equals(databaseType) && isNull ? "byte[]" : DefaultType,

                (databaseType, isNull) =>
                    "tinyint".Equals(databaseType) && isNull == false
                        ? "byte"
                        : "tinyint".Equals(databaseType) && isNull ? "byte?" : DefaultType,

                (databaseType, isNull) =>
                    "varbinary".Equals(databaseType) && isNull == false
                        ? "byte[]"
                        : "varbinary".Equals(databaseType) && isNull ? "byte[]" : DefaultType,

                (databaseType, isNull) =>
                    "varchar".Equals(databaseType) && isNull == false
                        ? "string"
                        : "varchar".Equals(databaseType) && isNull ? "string" : DefaultType,

                (databaseType, isNull) =>
                    "varchar2".Equals(databaseType) && isNull == false
                        ? "string"
                        : "varchar2".Equals(databaseType) && isNull ? "string" : DefaultType,

                (databaseType, isNull) =>
                    "uniqueidentifier".Equals(databaseType) && isNull == false
                        ? "Guid"
                        : "uniqueidentifier".Equals(databaseType) && isNull ? "Guid?" : DefaultType,

                (databaseType, isNull) =>
                    "bit".Equals(databaseType) && isNull == false
                        ? "bool"
                        : "bit".Equals(databaseType) && isNull ? "bool?" : DefaultType
            };
        }
    }
}
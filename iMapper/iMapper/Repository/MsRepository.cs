using iMapper.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace iMapper.Repository
{
    public class MsRepository
    {
        private readonly string connectionString;
        private readonly string db;

        public MsRepository(string host, string db, string user, string password)
        {
            this.db = db;
            connectionString = $"Data Source={host};Initial Catalog={db};User ID={user};Password={password}";
        }

        public MsRepository(string host, string db)
        {
            this.db = db;
            connectionString = $"Initial Catalog={db};Data Source={host};Integrated Security=SSPI;";
        }

        public List<ColumnModel> GetColumns()
        {
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sql = @"SELECT
						  ISC.TABLE_CATALOG,
						  ISC.TABLE_SCHEMA,
						  ISC.TABLE_NAME,
						  ISC.COLUMN_NAME,
						  ISC.ORDINAL_POSITION,
						  ISC.IS_NULLABLE,
						  ISC.DATA_TYPE,
						  ISC.CHARACTER_MAXIMUM_LENGTH,

						  (SELECT
							 TOP 1 COUNT(*)
						  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS C
						  JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS K
							ON C.TABLE_NAME = K.TABLE_NAME
							AND C.CONSTRAINT_CATALOG = K.CONSTRAINT_CATALOG
							AND C.CONSTRAINT_SCHEMA = K.CONSTRAINT_SCHEMA
							AND C.CONSTRAINT_NAME = K.CONSTRAINT_NAME
						  WHERE C.CONSTRAINT_TYPE = 'PRIMARY KEY'
						  AND K.COLUMN_NAME = ISC.COLUMN_NAME
						  AND K.TABLE_NAME = ISC.TABLE_NAME
						  AND K.TABLE_CATALOG = '{0}')
						  AS PRIMARY_KEY,

						  (SELECT
							 TOP 1 RELATION_TABLE = PK.TABLE_NAME
						  FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
						  INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
							ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
						  INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
							ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
						  INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
							ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
						  INNER JOIN (SELECT
							i1.TABLE_NAME,
							i2.COLUMN_NAME
						  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
						  INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
							ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
						  WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT
							ON PT.TABLE_NAME = PK.TABLE_NAME
						  WHERE FK.TABLE_CATALOG = '{0}'
						  AND PK.TABLE_CATALOG = '{0}'
						  AND CU.TABLE_CATALOG = '{0}'
						  AND FK.TABLE_NAME = ISC.TABLE_NAME
						  AND CU.COLUMN_NAME = ISC.COLUMN_NAME)
						  AS RELATION_TABLE,

						  (SELECT
							 TOP 1 RELATION_COLUMN = PT.COLUMN_NAME
						  FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
						  INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
							ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
						  INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
							ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
						  INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
							ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
						  INNER JOIN (SELECT
							i1.TABLE_NAME,
							i2.COLUMN_NAME
						  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
						  INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
							ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
						  WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT
							ON PT.TABLE_NAME = PK.TABLE_NAME
						  WHERE FK.TABLE_CATALOG = '{0}'
						  AND PK.TABLE_CATALOG = '{0}'
						  AND CU.TABLE_CATALOG = '{0}'
						  AND FK.TABLE_NAME = ISC.TABLE_NAME
						  AND CU.COLUMN_NAME = ISC.COLUMN_NAME)
						  AS RELATION_COLUMN,

							(SELECT
							 TOP 1 C.CONSTRAINT_NAME
						  FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
						  INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
							ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
						  INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
							ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
						  INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
							ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
						  INNER JOIN (SELECT
							i1.TABLE_NAME,
							i2.COLUMN_NAME
						  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
						  INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
							ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
						  WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT
							ON PT.TABLE_NAME = PK.TABLE_NAME
						  WHERE FK.TABLE_CATALOG = '{0}'
						  AND PK.TABLE_CATALOG = '{0}'
						  AND CU.TABLE_CATALOG = '{0}'
						  AND FK.TABLE_NAME = ISC.TABLE_NAME
						  AND CU.COLUMN_NAME = ISC.COLUMN_NAME)
						  AS CONSTRAINT_NAME

						FROM INFORMATION_SCHEMA.COLUMNS ISC
						WHERE ISC.TABLE_CATALOG = '{0}'
						ORDER BY ISC.TABLE_NAME,
						ISC.ORDINAL_POSITION ASC";

            var command = new SqlCommand(string.Format(sql, db), sqlConnection);
            var reader = command.ExecuteReader();

            var model = new List<ColumnModel>();
            while (reader.Read())
            {
                var column = new ColumnModel();
                column.TableCatalog = reader.GetString(0);
                column.TableSchema = reader.GetString(1);
                column.TableName = reader.GetString(2);
                column.ColumnName = reader.GetString(3);
                column.OrdinalPosition = reader.GetInt32(4);
                column.IsNullable = "YES".Equals(reader.GetString(5));
                column.DataType = reader.GetString(6);
                column.CharacterMaximumLength = GetValue<int?>(7, reader);
                column.IsPrimaryKey = reader.GetInt32(8) != 0;
                column.IsRelation = string.IsNullOrEmpty(GetValue<string>(11, reader)) == false;
                column.RelationTable = GetValue<string>(9, reader);
                column.RelationColumn = GetValue<string>(10, reader);
                column.ConstraintName = GetValue<string>(11, reader);
                model.Add(column);
            }
            reader.Close();
            command.Dispose();
            sqlConnection.Close();
            return model;
        }

        public List<StoredProceduresModel> GetStoredProcedures()
        {
            var models = new List<StoredProceduresModel>();

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            const string sqlStored = @"SELECT name
					FROM dbo.sysobjects
					WHERE (TYPE = 'P')";

            var command = new SqlCommand(string.Format(sqlStored, db), sqlConnection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var name = GetValue<string>(0, reader);
                models.Add(new StoredProceduresModel { Name = name });
            }

            reader.Close();
            command.Dispose();

            const string sqlParameter = @"SELECT sys.procedures.name,
									   sys.parameters.name,
									   sys.parameters.parameter_id,
									   sys.parameters.system_type_id,
									   sys.types.is_nullable,
									   sys.types.name,
                                       sys.parameters.max_length,
                                       (select top(1) name from sys.schemas where schema_id = sys.procedures.schema_id) as schema_name
								FROM sys.parameters
								INNER JOIN sys.procedures ON parameters.object_id = procedures.object_id
								INNER JOIN sys.types ON parameters.system_type_id = types.system_type_id
								AND parameters.user_type_id = types.user_type_id";

            var commandParameter = new SqlCommand(string.Format(sqlParameter, db), sqlConnection);
            var readerParameter = commandParameter.ExecuteReader();

            while (readerParameter.Read())
            {
                var storedProceduresName = GetValue<string>(0, readerParameter);
                var parameterName = GetValue<string>(1, readerParameter);
                var parameterId = GetValue<int>(2, readerParameter);
                var isNull = GetValue<bool>(4, readerParameter);
                var dataType = GetValue<string>(5, readerParameter);
                var maxLength = GetValue<short?>(6, readerParameter);
                var schemaName = GetValue<string>(7, readerParameter);

                if (models.Any(x => x.Name == storedProceduresName))
                {
                    models.First(x => x.Name == storedProceduresName).SchemaName = schemaName;

                    models.First(x => x.Name == storedProceduresName).Parameters.Add(new StoredProceduresParameterModel
                    {
                        Id = parameterId,
                        Name = parameterName.Replace("@", string.Empty),
                        DataType = dataType,
                        IsNullable = isNull,
                        MaxLength = maxLength,
                        SchemaName = schemaName
                    });
                }
            }

            readerParameter.Close();
            commandParameter.Dispose();

            foreach (var model in models)
            {
                const string sql = @"SELECT	name,
											system_type_name,
											is_nullable,
											max_length
									FROM sys.dm_exec_describe_first_result_set_for_object
									(
									  OBJECT_ID('{0}.{1}'),
									  NULL
									);";

                string describeSql = string.Format(sql, model.SchemaName, model.Name);

                var describeCommand = new SqlCommand(string.Format(describeSql, db), sqlConnection);
                var describeReader = describeCommand.ExecuteReader();

                int index = 0;
                while (describeReader.Read())
                {
                    var columnName = GetValue<string>(0, describeReader);
                    var systemType = GetValue<string>(1, describeReader);
                    var isNull = GetValue<bool?>(2, describeReader);
                    var maxLength = GetValue<short?>(3, describeReader);

                    model.Columns.Add(new StoredProceduresColumnsModel
                    {
                        Id = index,
                        Name = columnName,
                        DataType = GetProcedureType(systemType),
                        IsNullable = isNull != null && isNull.Value,
                        MaxLength = maxLength
                    });

                    index += 1;
                }

                describeReader.Close();
                describeCommand.Dispose();
            }

            sqlConnection.Close();
            return models;
        }

        private static T GetValue<T>(int no, SqlDataReader reader)
        {
            return reader.IsDBNull(no) ? default(T) : reader.GetFieldValue<T>(no);
        }

        private static string GetProcedureType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return type;
            }

            var stringTypes = new List<string> { "nvarchar", "char", "nchar", "ntext", "text", "varchar" };
            foreach (var stringType in stringTypes.Where(stringType => type.IndexOf(stringType, StringComparison.Ordinal) >= 0))
            {
                return stringType;
            }

            var decimalTypes = new List<string> { "decimal", "numeric" };
            foreach (var decimalType in decimalTypes.Where(decimalType => type.IndexOf(decimalType, StringComparison.Ordinal) >= 0))
            {
                return decimalType;
            }

            return type;
        }
    }
}
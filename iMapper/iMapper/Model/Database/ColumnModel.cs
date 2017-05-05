namespace iMapper.Model.Database
{
    public class ColumnModel
    {
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int OrdinalPosition { get; set; }
        public bool IsNullable { get; set; }
        public string DataType { get; set; }
        public int? CharacterMaximumLength { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsRelation { get; set; }
        public string RelationTable { get; set; }
        public string RelationColumn { get; set; }
        public string ConstraintName { get; set; }
    }
}
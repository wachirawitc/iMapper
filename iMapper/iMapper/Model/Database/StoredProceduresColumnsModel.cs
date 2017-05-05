namespace iMapper.Model.Database
{
    public class StoredProceduresColumnsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public string DataType { get; set; }
        public int? MaxLength { get; set; }
    }
}
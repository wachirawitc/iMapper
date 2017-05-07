using System.Collections.Generic;

namespace iMapper.Model
{
    public class ClassModel
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public List<PropertyModel> Members { get; set; }

        public ClassModel()
        {
            Members = new List<PropertyModel>();
        }
    }
}
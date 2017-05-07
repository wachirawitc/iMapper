using System.Collections.Generic;

namespace iMapper.Model
{
    public class ClassElementModel
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public List<MemberElementModel> Members { get; set; }

        public ClassElementModel()
        {
            Members = new List<MemberElementModel>();
        }
    }
}
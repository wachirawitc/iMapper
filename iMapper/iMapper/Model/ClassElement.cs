using System.Collections.Generic;

namespace iMapper.Model
{
    public class ClassElement
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public List<MemberElement> Members { get; set; }

        public ClassElement()
        {
            Members = new List<MemberElement>();
        }
    }
}
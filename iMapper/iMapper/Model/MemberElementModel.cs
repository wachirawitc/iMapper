using EnvDTE;

namespace iMapper.Model
{
    public class MemberElementModel
    {
        public CodeElement Element { get; set; }

        public CodeTypeRef Type { get; set; }

        public string FullName { get; set; }
    }
}
using EnvDTE;
using System.Collections.Generic;

namespace iMapper.Model
{
    public class CodeClassModel
    {
        public ProjectItem ProjectItem { get; set; }

        public List<CodeClass> CodeClasses { get; set; }

        public CodeClassModel()
        {
            CodeClasses = new List<CodeClass>();
        }
    }
}
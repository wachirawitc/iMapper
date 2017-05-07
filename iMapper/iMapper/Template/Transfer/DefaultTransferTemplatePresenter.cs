using iMapper.Model;
using System.Linq;

namespace iMapper.Template.Transfer
{
    public partial class DefaultTransferTemplate
    {
        public ClassModel Source { get; set; }

        public ClassModel Destination { get; set; }

        public string Name { get; set; }

        public string Namespace { get; set; }

        public bool IsMatchInSource(PropertyModel destination)
        {
            return Source.Members.Any(x => x.Name == destination.Name && x.TypeFullName == destination.TypeFullName);
        }
    }
}
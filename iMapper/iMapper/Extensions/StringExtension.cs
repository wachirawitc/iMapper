using iMapper.Support.DataType;
using System.Linq;

namespace iMapper.Extensions
{
    public static class StringExtension
    {
        public static string GetMsType(this string value, bool isNull)
        {
            MsDataType ms = new MsDataType();
            var type = ms.Types.FirstOrDefault(x => x.EngineType == value && x.IsSupportNullable == isNull);
            if (type != null)
            {
                return type.FrameworkType;
            }
            return "object";
        }
    }
}
using iMapper.Support.DataType;

namespace iMapper.Extensions
{
    public static class StringExtension
    {
        public static string GetMsType(this string column, bool isNull)
        {
            var sharpType = new SharpType();

            foreach (var func in sharpType.Types)
            {
                string result = func(column, isNull);
                if (string.IsNullOrEmpty(result) == false)
                {
                    return result;
                }
            }

            return "object";
        }
    }
}
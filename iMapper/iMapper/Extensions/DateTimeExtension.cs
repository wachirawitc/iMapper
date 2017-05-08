using System;

namespace iMapper.Extensions
{
    public static class DateTimeExtension
    {
        public static bool IsSame(this DateTime dateTime1, DateTime dateTime2)
        {
            return DateTime.Compare(dateTime1, dateTime2) == 0;
        }

        public static bool IsEarlierThan(this DateTime dateTime1, DateTime dateTime2)
        {
            return DateTime.Compare(dateTime1, dateTime2) < 0;
        }

        public static bool IsLaterThan(this DateTime dateTime1, DateTime dateTime2)
        {
            return DateTime.Compare(dateTime1, dateTime2) > 0;
        }
    }
}
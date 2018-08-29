using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.AngleSharpProgram.Common.Util
{
    public static class DateTimeUtil
    {
        public static long DataTimeToLong(this DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = dt.Subtract(dtStart);
            long timeStamp = toNow.Ticks;
            timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
            return timeStamp;
        }
    }
}

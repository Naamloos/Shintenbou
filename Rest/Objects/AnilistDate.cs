using System;

namespace Shintenbou.Rest.Objects
{
    public class AnilistDate
    {
        public int? Year { get; private set; }

        public int? Month { get; private set; }

        public int? Day { get; private set; }

        public static implicit operator DateTimeOffset?(AnilistDate date)
        {
            if (date.Year is null && date.Month is null && date.Day is null)
                return null;

            var newDate = new DateTimeOffset();
            
            if (date.Year != null)
                newDate.AddYears(date.Year.Value - newDate.Year);

            if (date.Month != null)
                newDate.AddMonths(date.Month.Value - newDate.Month);

            if (date.Day != null)
                newDate.AddDays(date.Day.Value - newDate.Day);

            return newDate;
        }
    }
}

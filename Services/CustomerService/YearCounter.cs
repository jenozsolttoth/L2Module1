using System;
using ServiceInterfaces;

namespace Services.CustomerService
{
    public class YearCounter : IYearCounter
    {
        public int GetDifference(DateTime date1, DateTime date2)
        {
            return (int) ((date2 - date1).TotalDays / 365);
        }
    }
}

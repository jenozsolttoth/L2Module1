using System;

namespace ServiceInterfaces
{
    public interface IYearCounter
    {
        int GetDifference(DateTime date1, DateTime date2);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ICustomer
    {
        string Name { get; set; }
        int Type { get; set; }
    }
}

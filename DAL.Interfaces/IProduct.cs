using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IProduct
    {
        string Id { get; }
        string Name { get; }
        bool Found { get; }
        decimal Price { get; }
    }
}

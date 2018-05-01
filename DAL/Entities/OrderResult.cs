using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class OrderResult
    {
        public List<IProduct> FoundProducts { get; set; }
        public List<string> NotFoundProducts { get; set; }
    }
}

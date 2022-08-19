using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductShop.Class;
using static ProductShop.Class.EF;
using ProductShop.EF;

namespace ProductShop.EF
{
    public partial class Customer
    {
        public string CustomerFullName { get => $"{LastName} {FirstName} {Patronymic}"; }
    }
}

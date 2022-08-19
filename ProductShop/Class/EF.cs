using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductShop.EF;
using ProductShop.Win;

namespace ProductShop.Class
{
    public static partial class EF
    {
        public static ProductShopEntities Context { get; } = new ProductShopEntities();
    }
}

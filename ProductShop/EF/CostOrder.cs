//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductShop.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostOrder
    {
        public int IdOrder { get; set; }
        public string NameProd { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}

namespace Maticsoft.ViewModel.Shop
{
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SalesModel
    {
        public List<SalesItem> SalesItems { get; set; }

        public Maticsoft.Model.Shop.Sales.SalesRule SalesRule { get; set; }
    }
}


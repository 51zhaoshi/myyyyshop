namespace Maticsoft.ViewModel.Shop
{
    using System;
    using System.Runtime.CompilerServices;

    public class SaleRecord
    {
        public int BuyCount { get; set; }

        public DateTime BuyDate { get; set; }

        public string BuyName { get; set; }

        public decimal BuyPrice { get; set; }
    }
}


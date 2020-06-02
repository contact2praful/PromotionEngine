﻿using System.Collections.Generic;

namespace PromotionalEngineAPI.Models
{
    //TODO: Shared by the consumer app to process the promotions on cart Items
    public class CartOrderContract
    {
        public List<SKU> CartOrders { get; set; }
    }

    public class SKU
    {
        public string SKUId { get; set; }
        public double Cost { get; set; }
        public double TotalSavings { get; set; }
    }

    //TODO: Needs to share this contract with Client to consume this API result
    public class CartOrderContractAfterPromotionApplied
    {
        public List<SKU> ProcessedCartOrder { get; set; }
    }
}
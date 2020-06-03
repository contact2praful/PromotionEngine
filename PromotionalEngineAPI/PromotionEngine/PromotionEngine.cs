using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionalEngineAPI.Models;

namespace PromotionalEngineAPI.PromotionEngine
{
    public class OnOneSKUPromotion : IPromotionType
    {
        public SKU CalculatePromotionForItem(PromotionType promotionType, PromotionModel promotionModel, SKU sku)
        {
            SKU _sku = new SKU();
            if (promotionType == PromotionType.OnOneSKU)
            {
                if (promotionModel.QutyOfSKUCollection.FirstOrDefault().Value > sku.Quty)
                {
                    var rem = 0;
                    var cost = (Math.DivRem(sku.Quty, promotionModel.QutyOfSKUCollection.FirstOrDefault().Value,out rem) * 
                               promotionModel.CostCollection.FirstOrDefault().Value) + (rem * sku.CostPerSKU);
                    _sku.TotalSavings = _sku.TotalCost - cost;
                    _sku.TotalCost = cost;
                }
            }
            return _sku;
        }
    }


    //Scenario-C is not completed below, still need to implement; leaving it as it here since time constraint
    public class OnMultipleSKUsFixedPrice : IPromotionType
    {
        public SKU CalculatePromotionForItem(PromotionType promotionType, PromotionModel promotionModel, SKU sku)
        {
            SKU _sku = new SKU();
            if (promotionType == PromotionType.OnOneSKU)
            {
                if (promotionModel.QutyOfSKUCollection.FirstOrDefault().Value > sku.Quty)
                {
                    var rem = 0;
                    var cost = (Math.DivRem(sku.Quty, promotionModel.QutyOfSKUCollection.FirstOrDefault().Value, out rem) *
                                promotionModel.CostCollection.FirstOrDefault().Value) + (rem * sku.CostPerSKU);
                    _sku.TotalSavings = _sku.TotalCost - cost;
                    _sku.TotalCost = cost;
                }
            }
            return _sku;
        }
    }
}

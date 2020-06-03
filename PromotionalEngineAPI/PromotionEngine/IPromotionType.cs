using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionalEngineAPI.Models;

namespace PromotionalEngineAPI.PromotionEngine
{
    public interface IPromotionType
    {
        public SKU CalculatePromotionForItem(PromotionType promotionType, PromotionModel promotionModel, SKU sku);
    }
}

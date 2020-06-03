using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace PromotionalEngineAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class Pramotions
    {
        public PromotionModel[] PromotionModels { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PromotionModel
    {
        [JsonProperty("Identifier")]
        public string Identifier { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("SKUCollection")]
        public Dictionary<string, string> SKUCollection { get; set; }

        [JsonProperty("OperatorCollection")]
        public Dictionary<string, string> OperatorCollection { get; set; }

        [JsonProperty("QutyOfSKUCollection")]
        public Dictionary<string, int> QutyOfSKUCollection { get; set; }

        [JsonProperty("CostCollection")]
        public Dictionary<string, int> CostCollection { get; set; }
    }

    public enum PromotionType
    {
        OnOneSKU = 0,
        OnMultipleSKUsFixedPrice = 1,
        OnTotalAmountInPercent = 2,
        OnSingleSKUInPercent = 3
    }
}

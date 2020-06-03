using System;
using MediatR;
using PromotionalEngineAPI.Models;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using PromotionalEngineAPI.PromotionEngine;

namespace PromotionalEngineAPI.Handlers
{
    public class Query : IRequest<CartOrderContractAfterPromotionApplied>
    {
        public CartOrderContract CartOrderContratContract { get; set; }
    }
    public class PrmotionFeedHandler : IRequestHandler<Query, CartOrderContractAfterPromotionApplied>
    {
        private readonly ILogger _logger;
        private readonly CancellationToken _stoppingToken;
        private const string FileName = "Promotions.json";
        private IPromotionType _promotionHandler;

        public PrmotionFeedHandler(ILogger logger)
        {
            _logger = logger;
            _stoppingToken = new CancellationToken();
        }

        public async Task<CartOrderContractAfterPromotionApplied> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                //Collect Valid SKUs for Promotion
                var promotions = JsonConvert.DeserializeObject<Pramotions>(JsonConvert.SerializeObject(
                    JObject.Parse(File.ReadAllText(FileName)),
                    Formatting.Indented));

                List<SKU> tempCartPromoApplied = new List<SKU>();
                Dictionary<string, string> SKU = new Dictionary<string, string>();
                Dictionary<string, string> OPERATOR = new Dictionary<string, string>();
                Dictionary<string, int> QTY = new Dictionary<string, int>();

                //"Open Close Principal" can be applied here
                //This can be refactored in a way that this class dont need to modify when new Promotion Type introduce
                //Leaving this implementation as is because of time constraint
                foreach (var promotionsModel in promotions.PromotionModels)
                {
                    if (promotionsModel.Type == PromotionType.OnOneSKU.ToString())
                    {
                        //This engine object can be loaded runtime by implementing Factory Pattern
                        _promotionHandler = new OnOneSKUPromotion();
                        foreach (var orderItem in request.CartOrderContratContract.CartOrder)
                        {
                            foreach (var sku in promotionsModel.SKUCollection)
                            {
                                if (sku.Value == orderItem.SKUId)
                                {
                                    var result = _promotionHandler.CalculatePromotionForItem(PromotionType.OnOneSKU,
                                        promotionsModel, orderItem);
                                }
                            }
                        }
                    }

                    if (promotionsModel.Type == PromotionType.OnMultipleSKUsFixedPrice.ToString())
                    {
                        //This engine object can be loaded runtime by implementing Factory Pattern
                        _promotionHandler = new OnMultipleSKUsFixedPrice();
                        foreach (var orderItem in request.CartOrderContratContract.CartOrder)
                        {
                            foreach (var sku in promotionsModel.SKUCollection)
                            {
                                if (sku.Value == orderItem.SKUId)
                                {
                                    var result = _promotionHandler.CalculatePromotionForItem(PromotionType.OnOneSKU,
                                        promotionsModel, orderItem);
                                }
                            }
                        }
                    }
                }

                await Task.CompletedTask;
                return new CartOrderContractAfterPromotionApplied();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to handle feed for {request}", request);
                throw;
            }
        }

        public static StringContent GetStringContent
            => new StringContent(JsonConvert.SerializeObject(
                JObject.Parse(File.ReadAllText(FileName)),
                Formatting.Indented), Encoding.Default, "application/json");
    }
}

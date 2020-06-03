using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using PromotionalEngineAPI.Handlers;
using PromotionalEngineAPI.Models;
using Serilog;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace PromotionalEngineAPI.UnitTests.Handlers
{
    public class PrmotionFeedHandlerTests
    {
        private PrmotionFeedHandler _sut;
        private readonly ILogger _loggerMock;

        public PrmotionFeedHandlerTests(ITestOutputHelper output)
        {
            _loggerMock = output.CreateTestLogger();
            _sut = new PrmotionFeedHandler(_loggerMock);
        }

        [Fact]
        public async Task Should_process_cart_data_for_promotions()
        {
            //Scenario-A
            var query_scanrio_A = GetMockQueryForScenario_A();
            var result = await _sut.Handle(query_scanrio_A, CancellationToken.None);

            foreach (var item in result.ProcessedCartOrder)
            {
                if(item.SKUId=="A") item.TotalSavings.ShouldBe(50);
                if(item.SKUId=="B") item.TotalSavings.ShouldBe(30);
                if(item.SKUId=="C") item.TotalSavings.ShouldBe(20);
            }

            //Scenario-B
            var query_scanrio_B = GetMockQueryForScenario_B();
            result = await _sut.Handle(query_scanrio_B, CancellationToken.None);

            foreach (var item in result.ProcessedCartOrder)
            {
                if (item.SKUId == "A") item.TotalSavings.ShouldBe(230);
                if (item.SKUId == "B") item.TotalSavings.ShouldBe(120);
                if (item.SKUId == "C") item.TotalSavings.ShouldBe(20);
            }

            //Scenario-C
            var query_scanrio_C = GetMockQueryForScenario_C();
            result = await _sut.Handle(query_scanrio_C, CancellationToken.None);

            foreach (var item in result.ProcessedCartOrder)
            {
                if (item.SKUId == "A") item.TotalSavings.ShouldBe(130);
                if (item.SKUId == "B") item.TotalSavings.ShouldBe(120);
                if (item.SKUId == "C") item.TotalSavings.ShouldBe(0);
                if (item.SKUId == "D") item.TotalSavings.ShouldBe(30);
            }

        }


        //Not completed because of time constraint
        //[Fact]
        //public async Task Should_throw_an_exception_if_cartData_null()
        //{

        //}

        private static Query GetMockQueryForScenario_A()
        {
            return new Query()
            {
                CartOrderContratContract = new CartOrderContract()
                {
                    CartOrder = new List<SKU>
                    {
                        new SKU()
                        {
                            SKUId = "A", CostPerSKU = 50, Quty = 1, TotalSavings = 0, TotalCost = 50
                        },
                        new SKU()
                        {
                            SKUId = "B", CostPerSKU = 30, Quty = 1, TotalSavings = 0, TotalCost = 30
                        },
                        new SKU()
                        {
                            SKUId = "C", CostPerSKU = 20, Quty = 1, TotalSavings = 0, TotalCost = 20
                        }
                    }
                }
            };
        }

        private static Query GetMockQueryForScenario_B()
        {
            return new Query()
            {
                CartOrderContratContract = new CartOrderContract()
                {
                    CartOrder = new List<SKU>
                    {
                        new SKU()
                        {
                            SKUId = "A", CostPerSKU = 50, Quty = 5, TotalSavings = 0, TotalCost = 250
                        },
                        new SKU()
                        {
                            SKUId = "B", CostPerSKU = 30, Quty = 5, TotalSavings = 0, TotalCost = 150
                        },
                        new SKU()
                        {
                            SKUId = "C", CostPerSKU = 20, Quty = 1, TotalSavings = 0, TotalCost = 20
                        }
                    }
                }
            };
        }

        private static Query GetMockQueryForScenario_C()
        {
            return new Query()
            {
                CartOrderContratContract = new CartOrderContract()
                {
                    CartOrder = new List<SKU>
                    {
                        new SKU()
                        {
                            SKUId = "A", CostPerSKU = 50, Quty = 3, TotalSavings = 0, TotalCost = 150
                        },
                        new SKU()
                        {
                            SKUId = "B", CostPerSKU = 30, Quty = 5, TotalSavings = 0, TotalCost = 150
                        },
                        new SKU()
                        {
                            SKUId = "C", CostPerSKU = 20, Quty = 1, TotalSavings = 0, TotalCost = 20
                        },
                        new SKU()
                        {
                            SKUId = "D", CostPerSKU = 30, Quty = 1, TotalSavings = 0, TotalCost = 30
                        }
                    }
                }
            };
        }
    }


    [ExcludeFromCodeCoverage]
    public static class TestLoggerFactory
    {
        public static ILogger CreateTestLogger(this ITestOutputHelper output)
            => new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.TestCorrelator().WriteTo.TestOutput(output).CreateLogger();
    }
}

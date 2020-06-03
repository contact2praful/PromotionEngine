using System;
using MediatR;
using PromotionalEngineAPI.Models;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

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

                await Task.CompletedTask;
                return new CartOrderContractAfterPromotionApplied();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to handle feed for {request}", request);
                throw;
            }
        }
    }
}

using MediatR;
using PromotionalEngineAPI.Models;

namespace PromotionalEngineAPI.Handlers
{
    public class Query : IRequest<CartOrderContractAfterPromotionApplied>
    {
        public CartOrderContract CartOrderContratContract { get; set; }
    }
    public class PrmotionFeedHandler
    {
    }
}

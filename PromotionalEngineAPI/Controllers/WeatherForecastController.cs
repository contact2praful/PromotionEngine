using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PromotionalEngineAPI.Models;
using Serilog;
using MediatR;
using PromotionalEngineAPI.Handlers;

namespace PromotionalEngineAPI.Controllers
{
    [ApiController]
    [Route("PromotionalEngine/PromotionAPI")]
    public class PromotionalEngineController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public PromotionalEngineController(IMediator mediator, ILogger logger)
        {
            _logger = logger.ForContext<PromotionalEngineController>();
            _mediator = mediator;
        }

        [HttpPost]
        [Route("PostOrderData")]
        public async Task<IActionResult> PostOrderFeed([FromBody]object request)
        {
            try
            {
                _logger.Information("Received product feed from Cart for promotion calculation event");
                _logger.Debug("Post Od=rder Feed event request {request}", request);

                var orderFeed = JsonConvert.DeserializeObject<CartOrderContract>(request.ToString());
                if (!orderFeed.CartOrders.Any())
                {
                    return StatusCode(400, "No results found in product feed for promotion calculation event");
                }

                var result = await _mediator.Send(new Query
                {
                    CartOrderContratContract = orderFeed
                });
                return Ok(result);

            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Failed to process product feed for promotion calculation event", request);
                return StatusCode(500, $"Error processing product feed for promotion calculation event {exception.Message}");
            }
        }
    }
}

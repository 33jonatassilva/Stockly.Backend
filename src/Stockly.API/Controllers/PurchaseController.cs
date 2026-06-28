using Microsoft.AspNetCore.Mvc;

namespace Stockly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPurchase(string purchaseId)
        {

            return Ok(new { Message = $"Purchase details for ID: {purchaseId}" });

        }
    }
}

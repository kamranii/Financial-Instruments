using Microsoft.AspNetCore.Mvc;
using TestCase.Core.Interfaces;
using TestCase.Services.Interfaces;

namespace TestCase.Controllers
{
    [Route("api/[controller]")]
    public class InstrumentsController : Controller
    {
        private readonly IPriceService _priceService;
        private readonly ILogger<InstrumentsController> _logger;

        public InstrumentsController(ILogger<InstrumentsController> logger, 
                                     IPriceService priceService)
        {
            _logger = logger;
            _priceService = priceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstruments()
        {
            var instruments = await _priceService.GetInstrumentsAsync();
            return Ok(instruments);
        }

        [HttpGet("{instrument}")]
        public async Task<IActionResult> GetPrice(string instrument)
        {
            var price = await _priceService.GetPriceAsync(instrument);
            if (price is null)
                return NotFound();

            return Ok(price);
        }
    }
}


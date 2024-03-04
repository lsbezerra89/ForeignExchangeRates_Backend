using AV.ForeignExchangeRates.Application.Interfaces;
using AV.ForeignExchangeRates.Application.Models.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AV.ForeignExchangeRates.API.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyAppService _currencyAppService;
        private readonly IValidator<ExchangeRateRequest> _exchangeRateValidator;
        private readonly IValidator<CurrencyRequest> _currencyRequestValidator;
        private readonly IValidator<UpdateNameRequest> _updateNameRequestValidator;
        private readonly IValidator<CreateCurrencyRequest> _createCurrencyRequestValidator;

        public CurrencyController(ICurrencyAppService currencyAppService, IValidator<ExchangeRateRequest> getExchangeRateValidator, IValidator<CurrencyRequest> currencyRequestValidator, IValidator<UpdateNameRequest> updateNameRequestValidator, IValidator<CreateCurrencyRequest> createCurrencyRequestValidator)
        {
            _currencyAppService = currencyAppService;
            _exchangeRateValidator = getExchangeRateValidator;
            _currencyRequestValidator = currencyRequestValidator;
            _updateNameRequestValidator = updateNameRequestValidator;
            _createCurrencyRequestValidator = createCurrencyRequestValidator;
        }

        [HttpGet("get-exchange")]
        public async Task<IActionResult> GetExchangeRate([FromQuery] ExchangeRateRequest request)
        {
            var validation = _exchangeRateValidator.Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var response = await _currencyAppService.GetExchangeRate(request);

            return Ok(response);
        }

        [HttpGet("/{Code}")]
        public async Task<IActionResult> GetCurrency([FromRoute] CurrencyRequest request)
        {
            var validation = _currencyRequestValidator.Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var response = _currencyAppService.GetCurrency(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] CreateCurrencyRequest request)
        {
            var validation = _createCurrencyRequestValidator.Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var response = _currencyAppService.CreateCurrency(request);

            return Ok(response);
        }


        [HttpPatch("/{Code}")]
        public async Task<IActionResult> UpdateCurrencyName([FromRoute] string Code, [FromBody] string name)
        {

            var request = new UpdateNameRequest()
            {
                Code = Code,
                Name = name
            };

            var validation = _updateNameRequestValidator.Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var response = _currencyAppService.UpdateCurrencyName(request);

            return Ok(response);
        }

        [HttpDelete("/{Code}")]
        public async Task<IActionResult> DeleteCurrency([FromRoute] CurrencyRequest request)
        {

            var validation = _currencyRequestValidator.Validate(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var response = _currencyAppService.DeleteCurrency(request);

            return Ok(response);

        }
    }
}

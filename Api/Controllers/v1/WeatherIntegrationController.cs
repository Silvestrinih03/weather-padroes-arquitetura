using Api.Controllers.Routes;
using Application.DTOs.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Route($"api/v{{version:apiVersion}}/{ApiRoutes.Integration}")]
    public class WeatherIntegrationController : BaseApiController
    {
        private readonly IIntegrationService _integrationService;

        public WeatherIntegrationController(
            IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }

        [HttpPost("FetchAndSaveWeatherToLocalDatabase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FetchAndSaveWeatherToLocalDatabase([FromBody] List<WeatherRequest> weatherRequest)
        {
            await _integrationService.FetchAndSaveWeatherToLocalDatabase(weatherRequest);

            return Ok();
        }
    }
}
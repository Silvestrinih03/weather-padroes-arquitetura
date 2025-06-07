using Api.Controllers.Routes;
using Application.DTOs.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Route($"api/v{{version:apiVersion}}/{ApiRoutes.Information}")]
    public class WeatherInformationController : BaseApiController
    {
        private readonly IInformationService _informationService;

        public WeatherInformationController(
            IInformationService informationService)
        {
            _informationService = informationService;
        }

        [HttpGet("GetWeather")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<WeatherResponse>> GetWeather()
        {
            return await _informationService.GetWeather();
        }
    }
}
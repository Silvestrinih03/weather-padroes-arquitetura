using Api.Controllers.Routes;
using Application.DTOs.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet("GetAllWeather")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<WeatherResponse>> GetAllWeather()
        {
            return await _informationService.GetAllWeather();
        }

        [HttpGet("GetAllWeatherFromProvider")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<WeatherResponse>> GetAllWeatherFromProvider([Required(ErrorMessage = "Provider is required")] string provider)
        {
            return await _informationService.GetAllWeatherFromProvider(provider);
        }

        [HttpGet("GetWeatherFromCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<WeatherResponse>> GetWeatherFromCity([Required(ErrorMessage = "City is required")] string city, string? state, string? country)
        {
            return await _informationService.GetWeatherFromCity(city, state, country);
        }
    }
}
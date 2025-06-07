using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Api.Controllers.v1
{
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        protected BaseApiController()
        { }
    }
}
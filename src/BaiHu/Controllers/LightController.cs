using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace BaiHu.Controllers;

[ApiController]
[Route("bai-hu/api/v1/[controller]")]
public class LightController : ControllerBase {
    private readonly ILogger<LightController> _logger;
    public LightController(ILogger<LightController> logger) {
        _logger = logger;
    }

    /// <summary>
    /// Query last known light state
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Light
    /// 
    /// </remarks>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// Query last known light state
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Light
    /// 
    /// </remarks>
    [HttpPut]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}

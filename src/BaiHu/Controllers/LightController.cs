using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Client;

namespace BaiHu.Controllers;

[ApiController]
[Route("bai-hu/api/v1/[controller]")]
public class LightController : ControllerBase {
    private readonly ILogger<LightController> _logger;
    private readonly IMqttClient _mqttClient;
    public LightController(ILogger<LightController> logger, IMqttClient mqttClient) {
        _logger = logger;
        _mqttClient = mqttClient;
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
        if (_mqttClient.IsConnected)
            await _mqttClient.PublishAsync(new MqttApplicationMessageBuilder().WithTopic("the-verse/test")
                                                                        .WithPayload("Hello World")
                                                                        .WithExactlyOnceQoS()
                                                                        .WithRetainFlag()
                                                                        .Build());
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}
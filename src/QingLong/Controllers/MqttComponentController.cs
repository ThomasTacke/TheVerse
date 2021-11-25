using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QingLong.Models;

namespace QingLong.Controllers;

[ApiController]
[Route("qing-long/api/v1/[controller]")]
public class MqttComponentController : ControllerBase {
    private readonly ILogger<MqttComponentController> _logger;
    private readonly DatabaseContext _context;

    public MqttComponentController(ILogger<MqttComponentController> logger, DatabaseContext databaseContext) {
        _logger = logger;
        _context = databaseContext;
    }

    /// <summary>
    /// Query MqttComponent Table
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /MqttComponent
    /// 
    /// </remarks>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<MqttComponent>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() {
        IEnumerable<MqttComponent> entities;
        int totalRecords = await _context.MqttComponents.CountAsync();
        if (totalRecords == 0) return StatusCode(StatusCodes.Status204NoContent);
        entities = await _context.MqttComponents.OrderBy(mqttComponent => mqttComponent.Id)
                                                .ToListAsync();
        return StatusCode(StatusCodes.Status200OK, entities);
    }

    /// <summary>
    /// Get Specific MqttComponent
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /MqttComponent/1
    /// 
    /// </remarks>
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponent), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id) {
        MqttComponent mqttComponent = await _context.MqttComponents.FindAsync(id);
        return mqttComponent != null ? StatusCode(StatusCodes.Status200OK, mqttComponent) : StatusCode(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Create a MqttComponent
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /MqttComponent
    ///     {
    ///         "topic": "base/room/device/type",
    ///         "mqttComponentTypeId": 1,
    ///         "deviceId": 1
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponent), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody][Required] MqttComponentForUploadDto mqttComponent) {
        MqttComponent mqttComponentEntity = new MqttComponent();
        mqttComponentEntity.Topic = mqttComponent.Topic;
        if (mqttComponent.DeviceId != 0) mqttComponentEntity.DeviceId = mqttComponent.DeviceId;
        if (mqttComponent.MqttComponentTypeId != 0) mqttComponentEntity.MqttComponentTypeId = mqttComponent.MqttComponentTypeId;
        await _context.MqttComponents.AddAsync(mqttComponentEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, mqttComponentEntity);
    }

    /// <summary>
    /// Update a MqttComponent
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /MqttComponent
    ///     {
    ///         "id": 1    
    ///         "mqttComponentTypeId": 2,
    ///         "deviceId": 3
    ///     }
    /// 
    /// </remarks>
    [HttpPut]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponent), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody][Required] MqttComponentForUpdateDto mqttComponent) {
        MqttComponent mqttComponentEntity = await _context.MqttComponents.FindAsync(mqttComponent.Id);
        if (!String.IsNullOrEmpty(mqttComponent.Topic)) mqttComponentEntity.Topic = mqttComponent.Topic;
        if (mqttComponent.DeviceId != 0) mqttComponentEntity.DeviceId = mqttComponent.DeviceId;
        if (mqttComponent.MqttComponentTypeId != 0) mqttComponentEntity.MqttComponentTypeId = mqttComponent.MqttComponentTypeId;
        _context.MqttComponents.Update(mqttComponentEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status200OK, mqttComponentEntity);
    }

    /// <summary>
    /// Delete a Specific MqttComponent
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /MqttComponent/1
    /// 
    /// </remarks>
    [HttpDelete("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        MqttComponent mqttComponent = await _context.MqttComponents.FindAsync(id);
        if (mqttComponent == null) return StatusCode(StatusCodes.Status404NotFound);
        _context.MqttComponents.Remove(mqttComponent);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }
}

public class MqttComponentForUploadDto {
    [Required(ErrorMessage = "Topic is required")]
    public string Topic { get; set; }
    [Required(ErrorMessage = "MqttComponentTypeId is required")]
    public int MqttComponentTypeId { get; set; }
    [Required(ErrorMessage = "DeviceId is required")]
    public int DeviceId { get; set; }
}

public class MqttComponentForUpdateDto {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    public string Topic { get; set; }
    public int MqttComponentTypeId { get; set; }
    public int DeviceId { get; set; }
}

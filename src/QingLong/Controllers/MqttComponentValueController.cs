using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QingLong.Models;

namespace QingLong.Controllers;

[ApiController]
[Route("qing-long/api/v1/[controller]")]
public class MqttComponentValueController : ControllerBase {
    private readonly ILogger<MqttComponentValueController> _logger;
    private readonly DatabaseContext _context;

    public MqttComponentValueController(ILogger<MqttComponentValueController> logger, DatabaseContext databaseContext) {
        _logger = logger;
        _context = databaseContext;
    }

    /// <summary>
    /// Query MqttComponentValue Table
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /MqttComponentValue
    /// 
    /// </remarks>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<MqttComponentValue>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() {
        IEnumerable<MqttComponentValue> entities;
        int totalRecords = await _context.MqttComponentValues.CountAsync();
        if (totalRecords == 0) return StatusCode(StatusCodes.Status204NoContent);
        entities = await _context.MqttComponentValues.OrderBy(mqttComponentValue => mqttComponentValue.Id)
                                                     .ToListAsync();
        return StatusCode(StatusCodes.Status200OK, entities);
    }

    /// <summary>
    /// Get Specific MqttComponentValue
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /MqttComponentValue/1
    /// 
    /// </remarks>
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponentValue), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id) {
        MqttComponentValue mqttComponentValue = await _context.MqttComponentValues.FindAsync(id);
        return mqttComponentValue != null ? StatusCode(StatusCodes.Status200OK, mqttComponentValue) : StatusCode(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Create a MqttComponentValue
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /MqttComponentValue
    ///     {
    ///         "value": "42.5",
    ///         "type": "°C",
    ///         "mqttComponentId": 1
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponentValue), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody][Required] MqttComponentValueForUploadDto mqttComponentValue) {
        MqttComponentValue mqttComponentValueEntity = new MqttComponentValue();
        mqttComponentValueEntity.Value = mqttComponentValue.Value;
        mqttComponentValueEntity.Type = mqttComponentValue.Type;
        if (mqttComponentValue.MqttComponentId != 0) mqttComponentValueEntity.MqttComponentId = mqttComponentValue.MqttComponentId;
        await _context.MqttComponentValues.AddAsync(mqttComponentValueEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, mqttComponentValueEntity);
    }

    /// <summary>
    /// Update a MqttComponentValue
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /MqttComponentValue
    ///     {
    ///         "id": 1,    
    ///         "mqttComponentId": 2
    ///     }
    /// 
    /// </remarks>
    [HttpPut]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponentValue), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody][Required] MqttComponentValueForUpdateDto mqttComponentValue) {
        MqttComponentValue mqttComponentValueEntity = await _context.MqttComponentValues.FindAsync(mqttComponentValue.Id);
        if (!String.IsNullOrEmpty(mqttComponentValue.Value)) mqttComponentValueEntity.Value = mqttComponentValue.Value;
        if (!String.IsNullOrEmpty(mqttComponentValue.Type)) mqttComponentValueEntity.Type = mqttComponentValue.Type;
        if (mqttComponentValue.MqttComponentId != 0) mqttComponentValueEntity.MqttComponentId = mqttComponentValue.MqttComponentId;
        _context.MqttComponentValues.Update(mqttComponentValueEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status200OK, mqttComponentValueEntity);
    }

    /// <summary>
    /// Delete a Specific MqttComponentValue
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /MqttComponentValue/1
    /// 
    /// </remarks>
    [HttpDelete("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        MqttComponentValue mqttComponentValue = await _context.MqttComponentValues.FindAsync(id);
        if (mqttComponentValue == null) return StatusCode(StatusCodes.Status404NotFound);
        _context.MqttComponentValues.Remove(mqttComponentValue);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }
}

public class MqttComponentValueForUploadDto {
    [Required(ErrorMessage = "Value is required")]
    public string Value { get; set; }
    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }
    [Required(ErrorMessage = "MqttComponentId is required")]
    public int MqttComponentId { get; set; }
}

public class MqttComponentValueForUpdateDto {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
    public int MqttComponentId { get; set; }
}

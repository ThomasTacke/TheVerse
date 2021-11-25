using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QingLong.Models;

namespace QingLong.Controllers;

[ApiController]
[Route("qing-long/api/v1/[controller]")]
public class MqttComponentTypeController : ControllerBase {
    private readonly ILogger<MqttComponentTypeController> _logger;
    private readonly DatabaseContext _context;

    public MqttComponentTypeController(ILogger<MqttComponentTypeController> logger, DatabaseContext databaseContext) {
        _logger = logger;
        _context = databaseContext;
    }

    /// <summary>
    /// Query MqttComponentType Table
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /MqttComponentType
    /// 
    /// </remarks>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<MqttComponentType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() {
        IEnumerable<MqttComponentType> entities;
        int totalRecords = await _context.MqttComponentTypes.CountAsync();
        if (totalRecords == 0) return StatusCode(StatusCodes.Status204NoContent);
        entities = await _context.MqttComponentTypes.OrderBy(mqttComponentType => mqttComponentType.Id)
                                                    .ToListAsync();
        return StatusCode(StatusCodes.Status200OK, entities);
    }

    /// <summary>
    /// Get Specific MqttComponentType
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /MqttComponentType/1
    /// 
    /// </remarks>
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponentType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id) {
        MqttComponentType mqttComponent = await _context.MqttComponentTypes.FindAsync(id);
        return mqttComponent != null ? StatusCode(StatusCodes.Status200OK, mqttComponent) : StatusCode(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Create a MqttComponentType
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /MqttComponentType
    ///     {
    ///         "type": "sensor"
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponentType), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody][Required] MqttComponentTypeForUploadDto mqttComponentType) {
        MqttComponentType mqttComponentTypeEntity = new MqttComponentType();
        mqttComponentTypeEntity.Type = mqttComponentType.Type;
        await _context.MqttComponentTypes.AddAsync(mqttComponentTypeEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, mqttComponentTypeEntity);
    }

    /// <summary>
    /// Update an MqttComponentType
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /MqttComponentType
    ///     {
    ///         "id": 1,    
    ///         "type": "actuator"
    ///     }
    /// 
    /// </remarks>
    [HttpPut]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(MqttComponentType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody][Required] MqttComponentTypeForUpdateDto mqttComponentType) {
        MqttComponentType mqttComponentTypeEntity = await _context.MqttComponentTypes.FindAsync(mqttComponentType.Id);
        if (!String.IsNullOrEmpty(mqttComponentType.Type)) mqttComponentTypeEntity.Type = mqttComponentType.Type;
        _context.MqttComponentTypes.Update(mqttComponentTypeEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status200OK, mqttComponentTypeEntity);
    }

    /// <summary>
    /// Delete an Specific MqttComonentType
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /MqttComponentType/1
    /// 
    /// </remarks>
    [HttpDelete("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        MqttComponentType mqttComponentType = await _context.MqttComponentTypes.FindAsync(id);
        if (mqttComponentType == null) return StatusCode(StatusCodes.Status404NotFound);
        _context.MqttComponentTypes.Remove(mqttComponentType);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }
}

public class MqttComponentTypeForUploadDto {
    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }
}

public class MqttComponentTypeForUpdateDto {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    public string Type { get; set; }
}

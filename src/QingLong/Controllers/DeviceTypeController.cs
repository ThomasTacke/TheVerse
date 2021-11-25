using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QingLong.Models;

namespace QingLong.Controllers;

[ApiController]
[Route("qing-long/api/v1/[controller]")]
public class DeviceTypeController : ControllerBase {
    private readonly ILogger<DeviceTypeController> _logger;
    private readonly DatabaseContext _context;

    public DeviceTypeController(ILogger<DeviceTypeController> logger, DatabaseContext databaseContext) {
        _logger = logger;
        _context = databaseContext;
    }

    /// <summary>
    /// Query DeviceType Table
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /DeviceType
    /// 
    /// </remarks>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<DeviceType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() {
        IEnumerable<DeviceType> entities;
        int totalRecords = await _context.DeviceTypes.CountAsync();
        if (totalRecords == 0) return StatusCode(StatusCodes.Status204NoContent);
        entities = await _context.DeviceTypes.OrderBy(deviceType => deviceType.Id)
                                             .ToListAsync();
        return StatusCode(StatusCodes.Status200OK, entities);
    }

    /// <summary>
    /// Get Specific DeviceType
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /DeviceType/1
    /// 
    /// </remarks>
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DeviceType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id) {
        DeviceType deviceType = await _context.DeviceTypes.FindAsync(id);
        return deviceType != null ? StatusCode(StatusCodes.Status200OK, deviceType) : StatusCode(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Create a DeviceType
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /DeviceType
    ///     {
    ///         "name": "NodeMCU"
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DeviceType), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody][Required] DeviceTypeForUploadDto deviceType) {
        DeviceType deviceTypeEntity = new DeviceType();
        deviceTypeEntity.Name = deviceType.Name;
        await _context.DeviceTypes.AddAsync(deviceTypeEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, deviceTypeEntity);
    }

    /// <summary>
    /// Update a DeviceType
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /DeviceType
    ///     {
    ///         "id": 1    
    ///         "name": "ArduinoUno",
    ///     }
    /// 
    /// </remarks>
    [HttpPut]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DeviceType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody][Required] DeviceTypeForUpdateDto deviceType) {
        DeviceType deviceTypeEntity = await _context.DeviceTypes.FindAsync(deviceType.Id);
        if (!String.IsNullOrEmpty(deviceType.Name)) deviceTypeEntity.Name = deviceType.Name;
        _context.DeviceTypes.Update(deviceTypeEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status200OK, deviceTypeEntity);
    }

    /// <summary>
    /// Delete a Specific DeviceType
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /DeviceType/1
    /// 
    /// </remarks>
    [HttpDelete("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        DeviceType deviceType = await _context.DeviceTypes.FindAsync(id);
        if (deviceType == null) return StatusCode(StatusCodes.Status404NotFound);
        _context.DeviceTypes.Remove(deviceType);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }
}

public class DeviceTypeForUploadDto {
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}

public class DeviceTypeForUpdateDto {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    public string Name { get; set; }
}

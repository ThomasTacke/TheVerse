using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QingLong.Models;

namespace QingLong.Controllers;

[ApiController]
[Route("qing-long/api/v1/[controller]")]
public class DeviceController : ControllerBase {
    private readonly ILogger<DeviceController> _logger;
    private readonly DatabaseContext _context;

    public DeviceController(ILogger<DeviceController> logger, DatabaseContext databaseContext) {
        _logger = logger;
        _context = databaseContext;
    }

    /// <summary>
    /// Query Device Table
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Device
    /// 
    /// </remarks>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Device>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() {
        IEnumerable<Device> entities;
        int totalRecords = await _context.Devices.CountAsync();
        if (totalRecords == 0) return StatusCode(StatusCodes.Status204NoContent);
        entities = await _context.Devices.OrderBy(device => device.Id)
                                         .ToListAsync();
        return StatusCode(StatusCodes.Status200OK, entities);
    }

    /// <summary>
    /// Get Specific Device
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Device/1
    /// 
    /// </remarks>
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Device), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id) {
        Device device = await _context.Devices.FindAsync(id);
        return device != null ? StatusCode(StatusCodes.Status200OK, device) : StatusCode(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Create a Device
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Device
    ///     {
    ///         "name": "motherloore01",
    ///         "displayName": "NodeMCU Kitchen",
    ///         "roomId": 1,
    ///         "deviceTypeId": 1
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Device), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody][Required] DeviceForUploadDto device) {
        Device deviceEntity = new Device();
        deviceEntity.Name = device.Name;
        deviceEntity.DisplayName = device.DisplayName;
        if (device.DeviceTypeId != 0) deviceEntity.DeviceTypeId = device.DeviceTypeId;
        if (device.RoomId != 0) deviceEntity.RoomId = device.RoomId;
        await _context.Devices.AddAsync(deviceEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, deviceEntity);
    }

    /// <summary>
    /// Update a Device
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /Device
    ///     {
    ///         "id": 1    
    ///         "displayName": "NodeMCU BedRoom",
    ///         "roomId": 2
    ///     }
    /// 
    /// </remarks>
    [HttpPut]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Device), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody][Required] DeviceForUpdateDto device) {
        Device deviceEntity = await _context.Devices.FindAsync(device.Id);
        if (!String.IsNullOrEmpty(device.Name)) deviceEntity.Name = device.Name;
        if (!String.IsNullOrEmpty(device.DisplayName)) deviceEntity.DisplayName = device.DisplayName;
        if (device.DeviceTypeId != 0) deviceEntity.DeviceTypeId = device.DeviceTypeId;
        if (device.RoomId != 0) deviceEntity.RoomId = device.RoomId;
        _context.Devices.Update(deviceEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status200OK, deviceEntity);
    }

    /// <summary>
    /// Delete a Specific Device
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /Device/1
    /// 
    /// </remarks>
    [HttpDelete("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        Device device = await _context.Devices.FindAsync(id);
        if (device == null) return StatusCode(StatusCodes.Status404NotFound);
        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }
}

public class DeviceForUploadDto {
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "DisplayName is required")]
    public string DisplayName { get; set; }
    [Required(ErrorMessage = "RoomId is required")]
    public int RoomId { get; set; }
    [Required(ErrorMessage = "DeviceTypeId is required")]
    public int DeviceTypeId { get; set; }
}

public class DeviceForUpdateDto {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public int RoomId { get; set; }
    public int DeviceTypeId { get; set; }
}

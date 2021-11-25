using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QingLong.Models;

namespace QingLong.Controllers;

[ApiController]
[Route("qing-long/api/v1/[controller]")]
public class RoomController : ControllerBase {
    private readonly ILogger<RoomController> _logger;
    private readonly DatabaseContext _context;

    public RoomController(ILogger<RoomController> logger, DatabaseContext databaseContext) {
        _logger = logger;
        _context = databaseContext;
    }

    /// <summary>
    /// Query Room Table
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Room
    /// 
    /// </remarks>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Room>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() {
        IEnumerable<Room> entities;
        int totalRecords = await _context.Rooms.CountAsync();
        if (totalRecords == 0) return StatusCode(StatusCodes.Status204NoContent);
        entities = await _context.Rooms.OrderBy(room => room.Id)
                                       .ToListAsync();
        return StatusCode(StatusCodes.Status200OK, entities);
    }

    /// <summary>
    /// Get Specific Room
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Room/1
    /// 
    /// </remarks>
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id) {
        Room room = await _context.Rooms.FindAsync(id);
        return room != null ? StatusCode(StatusCodes.Status200OK, room) : StatusCode(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Create a Room
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Room
    ///     {
    ///         "name": "Bed Room",
    ///         "shortName": "bd"
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Room), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody][Required] RoomForUploadDto room) {
        Room roomEntity = new Room();
        roomEntity.Name = room.Name;
        roomEntity.ShortName = room.ShortName;
        await _context.Rooms.AddAsync(roomEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, roomEntity);
    }

    /// <summary>
    /// Update a Room
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /Room
    ///     {
    ///         "id": 1    
    ///         "name": "Living Room",
    ///         "shortName": "lr"
    ///     }
    /// 
    /// </remarks>
    [HttpPut]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody][Required] RoomForUpdateDto room) {
        Room roomEntity = await _context.Rooms.FindAsync(room.Id);
        if (!String.IsNullOrEmpty(room.Name)) roomEntity.Name = room.Name;
        if (!String.IsNullOrEmpty(room.ShortName)) roomEntity.ShortName = room.ShortName;
        _context.Rooms.Update(roomEntity);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status200OK, roomEntity);
    }

    /// <summary>
    /// Delete a Specific Room
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /Room/1
    /// 
    /// </remarks>
    [HttpDelete("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        Room room = await _context.Rooms.FindAsync(id);
        if (room == null) return StatusCode(StatusCodes.Status404NotFound);
        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }
}

public class RoomForUploadDto {
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "ShortName is required")]
    public string ShortName { get; set; }
}

public class RoomForUpdateDto {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
}

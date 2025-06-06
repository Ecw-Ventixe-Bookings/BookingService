using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(BookingService bookingService) : ControllerBase
{
    private readonly BookingService _bookingService = bookingService;

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var result = await _bookingService.GetAllAsync();
        return result.Success
            ? Ok(result) 
            : BadRequest();
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(dto);

        var result = await _bookingService.CreateAsync(dto);
        return result.Success
            ? Ok(result)
            : BadRequest();
    }



    [HttpGet("count/{eventId}")]
    public async Task<IActionResult> GetCount(Guid eventId)
    {
        if (eventId == Guid.Empty) return BadRequest();

        var count = await _bookingService.GetTicketCountAsync(eventId);
        return Ok(new { count });
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserBookings(Guid userId)
    {
        if (userId == Guid.Empty) return BadRequest();
        var result = await _bookingService.GetUserBookings(userId);
        return result.Success
            ? Ok(result)
            : BadRequest();
    }
}

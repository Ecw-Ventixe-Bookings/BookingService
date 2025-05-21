using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookingsController(BookingService bookingService) : ControllerBase
{
    private readonly BookingService _bookingService = bookingService;

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var result = await _bookingService.GetAllAsync();
        return result.Success
            ? Ok(result) 
            : BadRequest();
    }

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
}

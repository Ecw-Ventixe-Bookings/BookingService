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

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var result = await _bookingService.GetAllAsync();
        return result.Success
            ? Ok(result) 
            : BadRequest();
    }

    // GetBookingsConnectedToUser(Email) <- User has to be loged in, no input for email.
    // on create, dto does not need email. user should be loged in, maybe change account to hold the other props as well?
    // AccountService should hold the contact information of the user, maybe set Auth:User:Id as the user ID on accountservice

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



    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetCount(Guid eventId)
    {
        if (eventId == Guid.Empty) return BadRequest();

        var count = await _bookingService.GetTicketCountAsync(eventId);
        return Ok(count);
    }
}

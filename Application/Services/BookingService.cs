using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class BookingService(IBookingRepository repo, EmailService emailService)
{
    private readonly IBookingRepository _repo = repo;
    private readonly EmailService _emailService = emailService;

    public async Task<Result> CreateAsync(CreateBookingDto dto)
    {
        var entity = new BookingEntity
        {
            EventId = dto.EventId,
            AccountId = dto.AccountId,
            TicketQuantity = dto.TicketQuantity,
            BookingDate = DateTime.Now,            
        };

        var result = await _repo.CreateAsync(entity);

        await _emailService.SendConfirmationEmailAsync(dto);

        return result.Success
            ? new Result() { Success = true }
            : new Result() { Success = false, ErrorMessage = "Booking could not be created" };
    }

    public async Task<Result<IEnumerable<BookingModel>>> GetAllAsync()
    {
        var result = await _repo.GetAllAsync();
        List<BookingModel> bookings = new();

        if (result.Success && result.Data is not null)
        {
            foreach (var booking in result.Data)
            {
                bookings.Add(new BookingModel
                {
                    Id = booking.Id,
                    EventId = booking.EventId,
                    AccountId = booking.AccountId,
                    TicketQuantity = booking.TicketQuantity,
                    BookingDate = booking.BookingDate
                });
            }
        }

        return result.Success
            ? new Result<IEnumerable<BookingModel>> { Success = true, Data = bookings }
            : new Result<IEnumerable<BookingModel>> { Success = false, ErrorMessage = "Could not get the bookings" };
    }

    public Task<Result<BookingModel>> GetAsync(Expression<Func<BookingEntity, bool>>? expression = null)
    {
        throw new NotImplementedException();
    }


    public async Task<Result> DeleteAsync(Guid id)
    {
        var repoResult = await _repo.GetAsync(b => b.Id == id);
        
        if (repoResult.Success && repoResult.Data is not null)
        {
            var result = await _repo.DeleteAsync(repoResult.Data);
            return result.Success
                ? result : new Result { Success = false, ErrorMessage = result.ErrorMessage };
        }
        return new Result { Success = false, ErrorMessage = repoResult.ErrorMessage };   
    }

    public async Task<int> GetTicketCountAsync(Guid id)
    {
        return await _repo.GetTicketCountAsync(id);
    }

    public async Task<Result<IEnumerable<BookingModel>>> GetUserBookings(Guid accountId)
    {
        var result = await _repo.GetAllAsync(x => x.AccountId == accountId);
        List<BookingModel> bookings = new();

        if (result.Success && result.Data is not null)
        {
            foreach (var booking in result.Data)
            {
                bookings.Add(new BookingModel
                {
                    Id = booking.Id,
                    EventId = booking.EventId,
                    AccountId = booking.AccountId,
                    TicketQuantity = booking.TicketQuantity,
                    BookingDate = booking.BookingDate
                });
            }
        }

        return result.Success
            ? new Result<IEnumerable<BookingModel>> { Success = true, Data = bookings }
            : new Result<IEnumerable<BookingModel>> { Success = false, ErrorMessage = "Could not get the bookings" };
    }
}

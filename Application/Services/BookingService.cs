using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class BookingService(IBookingRepository repo)
{
    private readonly IBookingRepository _repo = repo;

    public async Task<Result> CreateAsync(CreateBookingDto dto)
    {
        var entity = new BookingEntity
        {
            EventId = dto.EventId,
            TicketQuantity = dto.TicketQuantity,
            BookingDate = DateTime.Now,

            BookingOwner = new BookingOwnerEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,

                Address = new BookingOwnerAddressEntity
                {
                    StreetAddress = dto.StreetAddress,
                    PostalCode = dto.PostalCode,
                    City = dto.City,
                }
            }
        };


        var result = await _repo.CreateAsync(entity);
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
                    TicketQuantity = booking.TicketQuantity,
                    BookingDate = booking.BookingDate,
                    BookingOwnerId = booking.BookingOwnerId
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

    public Task<Result> UpdateAsync(UpdateBookingDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}

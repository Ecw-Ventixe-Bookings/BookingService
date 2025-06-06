using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

internal class BookingRepository(SqlServerDbContext context) : IBookingRepository
{
    private readonly SqlServerDbContext _context = context;
    public async Task<Result> CreateAsync(BookingEntity entity)
    {
        try
        {
            await _context.Bookings.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new Result { Success = true };
        }
        catch (Exception e)
        {
            return new Result { Success = false, ErrorMessage = $"Booking Could not be created: --- {e.Message}" };
        }
    }
    public async Task<Result<IEnumerable<BookingEntity>>> GetAllAsync(Expression<Func<BookingEntity, bool>>? expression = null)
    {
        try
        {
            IQueryable<BookingEntity> query = _context.Bookings;

            if (expression is not null)
                query = query.Where(expression);

            var entities = await query.ToListAsync();
            return new Result<IEnumerable<BookingEntity>> { Success = true, Data = entities };
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<BookingEntity>>() { Success = false, ErrorMessage = $"Failed to get all entities: {e.Message}" };
        }
    }
    public async Task<Result<BookingEntity>> GetAsync(Expression<Func<BookingEntity, bool>> expression)
    {
        try
        {
            var entity = await _context.Bookings.FirstOrDefaultAsync(expression);

            return entity is null
                ? new Result<BookingEntity> { Success = false, ErrorMessage = $"{expression} does not exist." }
                : new Result<BookingEntity> { Success = true, Data = entity };
        }
        catch (Exception e)
        {
            return new Result<BookingEntity>() { Success = false, ErrorMessage = $"Failed to get all entities: {e.Message}" };
        }
        
    }
    public async Task<Result> DeleteAsync(BookingEntity entity)
    {
        try
        {
            _context.Bookings.Remove(entity);
            int result = await _context.SaveChangesAsync();
            return result > 0
                ? new Result { Success = true, StatusCode = 200 }
                : new Result { Success = false, ErrorMessage = "Falied to remove the Booking" };
        }
        catch (Exception e)
        {
            return new Result { Success = false, ErrorMessage = "An exception happened when removing booking: " +  e.Message };
        }
    }
    ///// <summary>
    ///// Return the number of bookings for the given event ID
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    public async Task<int> GetTicketCountAsync(Guid id)
    {
        var count = await _context.Bookings
            .Where(x => x.EventId == id)
            .SumAsync(x => x.TicketQuantity);

        return count;
    }
}

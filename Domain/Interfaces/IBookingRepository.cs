

using Domain.Entities;
using Domain.Models;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IBookingRepository
{
    Task<Result> CreateAsync(BookingEntity entity);
    Task<Result<BookingEntity>> GetAsync(Expression<Func<BookingEntity, bool>> expression);
    Task<Result<IEnumerable<BookingEntity>>> GetAllAsync(Expression<Func<BookingEntity, bool>>? expression = null);
    Task<Result> DeleteAsync(BookingEntity entity);
    public Task<int> GetTicketCountAsync(Guid id);
}

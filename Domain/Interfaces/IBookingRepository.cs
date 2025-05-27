

using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookingRepository : IBaseRepository<BookingEntity>
{
    public Task<int> GetTicketCountAsync(Guid id);
}



namespace Domain.Entities;

public class BookingOwnerEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public BookingOwnerAddressEntity? Address { get; set; }

    public ICollection<BookingEntity>? Bookings { get; set; }
}

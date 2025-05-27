

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class BookingOwnerEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(200)]
    public string Email { get; set; } = null!;

    public BookingOwnerAddressEntity? Address { get; set; }

    public ICollection<BookingEntity>? Bookings { get; set; }
}

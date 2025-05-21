

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BookingEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventId { get; set; }
    public int TicketQuantity { get; set; }
    public DateTime BookingDate { get; set; }

    [ForeignKey(nameof(BookingOwner))]
    public Guid BookingOwnerId { get; set; }
    public BookingOwnerEntity BookingOwner { get; set; } = null!;
}

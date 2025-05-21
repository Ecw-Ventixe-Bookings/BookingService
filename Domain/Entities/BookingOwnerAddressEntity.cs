

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BookingOwnerAddressEntity
{
    [Key]
    [ForeignKey(nameof(BookingOwner))]
    public Guid BookingOwnerEntityId { get; set; }
    public string StreetAddress { get; set;} = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public BookingOwnerEntity BookingOwner { get; set; } = null!;
}

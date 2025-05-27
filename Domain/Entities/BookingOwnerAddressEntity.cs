

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BookingOwnerAddressEntity
{
    [Key]
    [ForeignKey(nameof(BookingOwner))]
    public Guid BookingOwnerEntityId { get; set; }

    [StringLength(100)]
    public string StreetAddress { get; set;} = null!;

    [StringLength(20)]
    public string PostalCode { get; set; } = null!;

    [StringLength(50)]
    public string City { get; set; } = null!;

    public BookingOwnerEntity BookingOwner { get; set; } = null!;
}

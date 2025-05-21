using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models;

public class CreateBookingDto
{
    public Guid EventId { get; set; }
    public int TicketQuantity { get; set; } = 1;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string StreetAddress { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

}

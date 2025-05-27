using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models;

public class CreateBookingDto
{
    public Guid EventId { get; set; }

    [Range(1, 50, ErrorMessage = "Max amount of tickets you can buy is 50")]
    public int TicketQuantity { get; set; } = 1;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email need to be formatted as <name@domain.com>")]
    public string Email { get; set; } = null!;

    public string StreetAddress { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

}

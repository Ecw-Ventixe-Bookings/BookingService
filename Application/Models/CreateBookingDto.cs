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
    public Guid AccountId { get; set; }

    [Range(1, 7, ErrorMessage = "Max amount of tickets you can buy is 7")]
    public int TicketQuantity { get; set; } = 1;

    public string AccountEmail { get; set; } = string.Empty;
}


/*
 * This json represent whats comming from the frontend

const data = {
    eventId: id,
    ticketQuantity: ticketQuantity,
    accountId: user.sub
}
*/
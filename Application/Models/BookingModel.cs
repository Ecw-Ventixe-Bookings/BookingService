

using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models;

public class BookingModel
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public Guid AccountId { get; set; }
    public int TicketQuantity { get; set; }
    public DateTime BookingDate { get; set; }
}

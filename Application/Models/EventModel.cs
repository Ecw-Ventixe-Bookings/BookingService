

using System.Text.Json.Serialization;

namespace Application.Models;

public class EventServiceResponse
{
    [JsonPropertyName("data")]
    public EventData Data { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; }
}

public class EventData
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("startDateTime")]
    public DateTime StartDateTime { get; set; }

    [JsonPropertyName("endDateTime")]
    public DateTime EndDateTime { get; set; }

    [JsonPropertyName("ticketPrice")]
    public decimal? TicketPrice { get; set; }

    [JsonPropertyName("totalTickets")]
    public int TotalTickets { get; set; }

    [JsonPropertyName("category")]
    public Category Category { get; set; }

    [JsonPropertyName("venue")]
    public Venue? Venue { get; set; }
}

public class Category
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}


public class Venue
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }
}



using Application.Factories;
using Application.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;

namespace Application.Services;

public class EmailService(IHttpClientFactory httpClientFactory, IConfiguration config, ILogger<BookingService> logger)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly IConfiguration _config = config;
    private readonly ILogger<BookingService> _logger = logger;

    public async Task SendConfirmationEmailAsync(CreateBookingDto dto)
    {
        var eventServiceResponse = await _httpClient.GetAsync($"{_config["Urls:EventService"]}/{dto.EventId}");
        eventServiceResponse.EnsureSuccessStatusCode();
        EventServiceResponse? response = await JsonSerializer.DeserializeAsync<EventServiceResponse>(
            await eventServiceResponse.Content.ReadAsStreamAsync());

        var client = new ServiceBusClient(_config["ESB:Connection"]);
        var emailSender = client.CreateSender("sendemail");

        var htmlContent = EmailContentProvider.BookingConfirmationHtml(
            response.Data.Title,
            response.Data.StartDateTime,
            $"{response.Data.Venue.Name}, {response.Data.Venue.Address}, {response.Data.Venue.City}",
            $"{_config["Urls:Frontend"]}/events/{dto.EventId}");

        var textContent = EmailContentProvider.BookingComfirmationText(
            response.Data.Title,
            response.Data.StartDateTime,
            $"{response.Data.Venue.Name}, {response.Data.Venue.Address}, {response.Data.Venue.City}",
            $"{_config["Urls:Frontent"]}/events/{dto.EventId}");


        var emailSenderBody = new
        {
            To = dto.AccountEmail,
            TextContent = textContent,
            HtmlContent = htmlContent
        };

        var emailSenderserviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(emailSenderBody));
        emailSenderserviceBusMessage.Subject = $"Tack för din bokning";
        emailSenderserviceBusMessage.ContentType = "application/json";


        await emailSender.SendMessageAsync(emailSenderserviceBusMessage);
        await emailSender.DisposeAsync();
        await client.DisposeAsync();    
    }
}

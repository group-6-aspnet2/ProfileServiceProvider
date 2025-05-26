
using Azure.Messaging.ServiceBus;
using Business.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Business.Services;

// Tagit hjälp av chatgpt
public interface IProfileServiceBusListener
{
    Task StartProcessingAsync();
}
public class ProfileServiceBusListener : IProfileServiceBusListener
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusProcessor _processor;
    private readonly IProfileService _profileService;

    public ProfileServiceBusListener(IConfiguration configuration, ServiceBusClient client, IProfileService profileService)
    {
        _client = client;
        _profileService = profileService;
        _processor = _client.CreateProcessor(configuration["ServiceBus:QueueName"], new ServiceBusProcessorOptions());

        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;
    }

    public async Task StartProcessingAsync()
    {
        await _processor.StartProcessingAsync();
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        Console.WriteLine($"Meddelande mottaget: {body}");

        try
        {
            var data = JsonSerializer.Deserialize<AccountCreatedMessage>(body);

            if (data != null && !string.IsNullOrWhiteSpace(data.UserId))
            {
                var model = new CreateProfileModel
                {
                    FirstName = "Olivia",
                    LastName = "Olsson",
                    Address = "Kantorsgatan 68",
                    PostalCode = 75424,
                    Role = "User"
                }; 

                await _profileService.CreateAsync(model, data.UserId);
            }

            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid hantering: {ex.Message}");
            await args.DeadLetterMessageAsync(args.Message);
        }
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Service Bus Error: {args.Exception.Message} ");
        return Task.CompletedTask;
    }

    public class AccountCreatedMessage
    {
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
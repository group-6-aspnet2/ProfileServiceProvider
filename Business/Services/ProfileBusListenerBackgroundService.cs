using Azure.Messaging.ServiceBus;
using Business.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Business.Services;

//AI-genererad kod 
public interface IProfileServiceBusHandler
{
    Task StartAsync();
}

public class ProfileServiceBusHandler : IProfileServiceBusHandler
{
    private readonly ServiceBusProcessor _processor;
    private readonly IProfileService _profileService;

    public ProfileServiceBusHandler(IConfiguration configuration, ServiceBusClient client, IProfileService profileService)
    {
        _profileService = profileService;

        _processor = client.CreateProcessor(configuration["AzureServiceBusSettings:CreateProfileQueueName"], new ServiceBusProcessorOptions());

        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;
    }

    public async Task StartAsync()
    {
        Console.WriteLine("[ServiceBus] Start listening for create-profile messages...");
        await _processor.StartProcessingAsync();
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        Console.WriteLine($"[ServiceBus] Received message: {body}");

        try
        {
            var data = JsonSerializer.Deserialize<AccountCreatedMessage>(body);

            if (data is not null && int.TryParse(data.UserId, out var userId))
            {
                var model = new CreateProfileModel
                {
                    FirstName = "Auto",
                    LastName = "Generated",
                    Address = "",
                    PostalCode = 12345,
                    Role = "User"
                };

                await _profileService.CreateAsync(model, userId);
                Console.WriteLine("[ServiceBus] Profile created for user " + userId);
            }

            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ServiceBus] ERROR: {ex.Message}");
            await args.DeadLetterMessageAsync(args.Message);
        }
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"[ServiceBus] Listener error: {args.Exception.Message}");
        return Task.CompletedTask;
    }

    private class AccountCreatedMessage
    {
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";
    }
}

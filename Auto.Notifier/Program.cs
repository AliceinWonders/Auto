
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Auto.Messages;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR.Client;
using JsonSerializer = System.Text.Json.JsonSerializer;

internal class Program
{
    private const string SIGNALR_HUB_URL = "https://localhost:7061/hub";
    private static HubConnection hub;

    static async Task Main(string[] args)
    {
        hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
        await hub.StartAsync();
        Console.WriteLine("Hub started!");
        Console.WriteLine("Press any key to send a message (Ctrl-C to quit)");
        var amqp = "amqp://user:rabbitmq@localhost:5672";
        using var bus = RabbitHutch.CreateBus(amqp);
        Console.WriteLine("Connected to bus! Listening for newVehicleMessages");
        var subscriberId = $"Auto.Notifier@{Environment.MachineName}";
        await bus.PubSub.SubscribeAsync<NewInfoAutoOwnerMessage>(subscriberId, HandleNewInfoAutoOwnerMessage);
        Console.ReadLine();
    }

    private static async void HandleNewInfoAutoOwnerMessage(NewInfoAutoOwnerMessage niaom)
    {
        var csvRow =
            $"{niaom.Name} {niaom.Age}";
        Console.WriteLine(csvRow);
        var json = JsonSerializer.Serialize(niaom, JsonSettings());
        await hub.SendAsync("NotifyWebUsers", "Auto.Notifier",
            json);
    }

    static JsonSerializerOptions JsonSettings() =>
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
}
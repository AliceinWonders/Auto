using System;
using System.Threading.Tasks;
using Auto.InfoOwner;
using Auto.Messages;
using Grpc.Net.Client;
using EasyNetQ;


class Program {
    private static Informer.InformerClient grpcClient;
    private static IBus bus;
    static async Task Main(string[] args) {
        Console.WriteLine("Starting Auto.InfoOwnerClient");
        var amqp ="amqp://user:rabbitmq@localhost:5672";
        bus = RabbitHutch.CreateBus(amqp);
        //Console.WriteLine("Connected to bus; Listening for newVehicleMessages");
        var grpcAddress = "http://localhost:5128";
        using var channel = GrpcChannel.ForAddress(grpcAddress);
        grpcClient = new Informer.InformerClient(channel);
        Console.WriteLine($"Connected to gRPC on {grpcAddress}!");
        var subscriberId = $"Auto.InfoOwnerClient@{Environment.MachineName}";
        await bus.PubSub.SubscribeAsync<NewAutoOwnerMessage>(subscriberId, HandleNewAutoOwnerMessage);
        Console.WriteLine("Press Enter to exit");
        Console.ReadLine();
    }

    private static async Task HandleNewAutoOwnerMessage(NewAutoOwnerMessage message)
    { 
        var request = new InfoRequest
        {
            Name = message.Name,
            Surname = message.Surname,
            Email = message.Email,
            Number = message.Number
        };
        var reply = await grpcClient.GetInfoAsync(request);
        Console.WriteLine($"AutoOwner's income is {reply.Income}");
        Console.WriteLine($"AutoOwner's age is {reply.Age}");
        var newInfoAutoOwnerMessage = new NewInfoAutoOwnerMessage(message, reply.Income, reply.Age);
        Console.WriteLine($"{newInfoAutoOwnerMessage.Name}, {newInfoAutoOwnerMessage.Age}, {newInfoAutoOwnerMessage.Income}");
        await bus.PubSub.PublishAsync(newInfoAutoOwnerMessage);
    }
}
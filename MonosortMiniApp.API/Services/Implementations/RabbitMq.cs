﻿using MonosortMiniApp.API.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client;
using System.Text.Json;

namespace MonosortMiniApp.API.Services.Implementations;

public class RabbitMq : IRabbitMq
{
    public async Task SendOrder(string message)
    {
        var factory = new ConnectionFactory { 
            HostName = Environment.GetEnvironmentVariable("RABBIT_HOST_NAME"),
            UserName = Environment.GetEnvironmentVariable("RABBIT_USER_NAME"),     // Из environment переменных
            Password = Environment.GetEnvironmentVariable("RABBIT_PASSWORD"), // Из environment переменных
            Port = Convert.ToInt32( Environment.GetEnvironmentVariable("RABBIT_PORT"))
        };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        {
            await channel.QueueDeclareAsync(
                queue: "order", 
                durable: true, 
                exclusive: false, 
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: string.Empty, 
                routingKey: "order",
                mandatory: true,
                basicProperties: properties,
                body: body);
        }
    }

    public void Serialize(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendOrder(message);
    }
}

using System.Threading;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Text;

namespace Site.Services
{
    public class AzureServiceBus : IEventService
    {
        private static string eventHubName = "voxpop";
        private static string connectionString = "RUH3abK1BkSOQgbxx04CUp8VrLcalAl//Rik01R6oEs=";

        static void SendRandomMessages(int numMessages)
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
            for(int i = 0; i < numMessages; i++)
            {
                try
                {
                    var message = Guid.NewGuid().ToString();
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, message);
                    eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }

                Thread.Sleep(200);
            }
        }
    }
}
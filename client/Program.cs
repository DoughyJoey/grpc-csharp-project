using Grpc.Core;
using SmartWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        const string target = "127.0.0.1:50053";

        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });

            var client = new WatchService.WatchServiceClient(channel);
            var stream = client.SleepAverage();

            foreach (int number in Enumerable.Range(6, 8))
            {
                var request = new SleepAverageRequest() { Number = number };

                await stream.RequestStream.WriteAsync(request);
            }

            await stream.RequestStream.CompleteAsync();

            var response = await stream.ResponseAsync;

            Console.WriteLine("Your average sleep this week is: " + response.Average + " hrs");

            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();


            // MAX HEART RATE
            var client2 = new WatchService.WatchServiceClient(channel);
            var stream2 = client.MaxHeartRate();

            var responseReaderTask = Task.Run(async () =>
            {
                while (await stream2.ResponseStream.MoveNext())
                    Console.WriteLine("Your current heart rate is: " + stream2.ResponseStream.Current.Maximum);
            });

            int[] numbers = { 66, 77, 73, 87, 91, 101 };

            foreach (var number in numbers)
            {
                await stream2.RequestStream.WriteAsync(new MaxHeartRateRequest() { Number = number });
            }

            await stream2.RequestStream.CompleteAsync();
            await responseReaderTask;

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}

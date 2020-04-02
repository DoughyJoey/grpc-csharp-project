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

            // CALORIES
            var calorieClient = new WatchService.WatchServiceClient(channel);

            var calorieRequest = new CalorieRequest()
            {
                FirstNumber = 2453,
                SecondNumber = 1710,
                ThirdNumber = 2300,
                FourthNumber = 1943,
                FifthNumber = 1856
            };

            var calorieResponse = calorieClient.Calories(calorieRequest);

            Console.WriteLine("Your total number of calories this week is: " + calorieResponse.SumResult);

            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            // AVERAGE SLEEP
            var sleepClient = new WatchService.WatchServiceClient(channel);
            var sleepStream = sleepClient.SleepAverage();

            foreach (int number in Enumerable.Range(6, 8))
            {
                var sleepRequest = new SleepAverageRequest() { Number = number };

                await sleepStream.RequestStream.WriteAsync(sleepRequest);
            }

            await sleepStream.RequestStream.CompleteAsync();

            var response = await sleepStream.ResponseAsync;

            Console.WriteLine("Your average sleep this week is: " + response.Average + " hrs");

            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();


            // MAX HEART RATE
            var maxHeartClient = new WatchService.WatchServiceClient(channel);
            var maxHeartStream = maxHeartClient.MaxHeartRate();

            var responseReaderTask = Task.Run(async () =>
            {
                while (await maxHeartStream.ResponseStream.MoveNext())
                    Console.WriteLine("Your current heart rate is: " + maxHeartStream.ResponseStream.Current.Maximum);
            });

            int[] numbers = { 66, 77, 73, 87, 91, 101 };

            foreach (var number in numbers)
            {
                await maxHeartStream.RequestStream.WriteAsync(new MaxHeartRateRequest() { Number = number });
            }

            await maxHeartStream.RequestStream.CompleteAsync();
            await responseReaderTask;

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}

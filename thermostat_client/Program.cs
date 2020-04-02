using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temp;

namespace thermostat_client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Channel channel = new Channel("localhost", 50054, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });

            var client = new TempService.TempServiceClient(channel);

            // SET TEMPERATURE
            var newTemp = SetTemp(client);

            // VIEW THE CURRENT TEMPERATURE
            ViewTemp(client);

            // CHANGE THE TEMPERATURE
            ChangeTemp(client, newTemp);

            // VIEW ALL TEMPERATURES IN THE DATABASE
            await ViewAllTemp(client);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        // SET TEMPERATURE
        private static Temp.Temp SetTemp(TempService.TempServiceClient client)
        {
            var response = client.SetTemp(new SetTempRequest()
            {
                Temp = new Temp.Temp()
                {
                    TempSetting = "24 degrees"
                }
            });

            Console.WriteLine("The temperature has been set to " + response.Temp.TempSetting + "!");

            return response.Temp;
        }

        // VIEW THE CURRENT TEMPERATURE
        private static void ViewTemp(TempService.TempServiceClient client)
        {
            try
            {
                var response = client.ViewTemp(new ViewTempRequest()
                {
                    TempId = "5e8372829a5b3c385007b6af"
                  //TempId = "5e8372829a5b3c385007b6ag"
                });

                Console.WriteLine("The current temperature is " + response.Temp.ToString());
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
            }
        }

        // CHANGE THE TEMPERATURE
        private static void ChangeTemp(TempService.TempServiceClient client, Temp.Temp temp)
        {
            try
            {
                temp.TempSetting = "30 degrees";

                var response = client.ChangeTemp(new ChangeTempRequest()
                {
                    Temp = temp
                });

                Console.WriteLine("The temperature has now been changed to " + response.Temp.ToString());
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
            }
        }

        // VIEW ALL TEMPERATURES
        private static async Task ViewAllTemp(TempService.TempServiceClient client)
        {
            var response = client.ViewAllTemp(new ViewAllTempRequest() { });

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Temp.ToString());
            }
        }
    }
}

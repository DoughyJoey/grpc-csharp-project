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
            /* var response = client.SetTemp(new SetTempRequest()
            {
                Temp = new Temp.Temp()
                {
                    TempSetting = "20 degrees"
                }
            });

            Console.WriteLine("The temperature " + response.Temp.Id + " was created!");
            Console.WriteLine("The temperature is now set to " + response.Temp.TempSetting + "!");*/

            try
            {
                var response = client.ViewTemp(new ViewTempRequest()
                {
                    TempId = "5e8372829a5b3c385007b6af"
                  //TempId = "5e8372829a5b3c385007b6ah"
                });
                Console.WriteLine(response.Temp.ToString());
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
            }
            
            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}

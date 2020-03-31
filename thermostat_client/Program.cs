using Grpc.Core;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thermostat;

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

            var client = new ThermostatService.ThermostatServiceClient(channel);
            var response = client.SetTemp(new SetTempRequest()
            {
                Temp = new Thermostat.Temp
                {
                    TempSetting = "22 celcius"

                }
            });

            Console.WriteLine("The temperature was set using id: " + response.Temp.Id);
            Console.WriteLine("Your temperature has now been set to: " + response.Temp.TempSetting);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}

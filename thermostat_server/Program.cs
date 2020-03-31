using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temp;

namespace thermostat_server
{
    class Program
    {
        const int Port = 50054;
        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server()
                {
                    Services = { TempService.BindService(new TempServiceImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };

                server.Start();
                Console.WriteLine("The server is listening on the port: " + Port);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("The server failed to start: " + e.Message);
                throw;
            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace serv
{
    class Program
    {
        static List<SocketHandler> clients = new List<SocketHandler>();
        static void MessageRoutine()
        {
            Task.Run(()=>{
                while(true)
                {
                    string str = Console.ReadLine();
                    foreach (var client in Program.clients)
                    {
                        client.SendMessage(str);
                    }
                }
            });
        }
        static async Task Run()
        {
            Socket s = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            EndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),5000);
            s.Bind(ep);
            s.Listen(10);
            while(true)
            {
                SocketHandler sh = new SocketHandler(await s.AcceptAsync());
                sh.BeginReceive();
                Program.clients.Add(sh);
            }
        }
        static void Main(string[] args)
        {
            MessageRoutine();
            Run().Wait();
        }
    }
}

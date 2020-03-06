using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("192.168.24.205");
            TcpListener serverSocket = new TcpListener(ip, 4646);
            serverSocket.Start();
            Console.WriteLine("Server Started and is running...");

            do
            {
                Task.Run(() =>
                {
                    TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                    Console.WriteLine("Server activated & Connected");
                    DoClient(connectionSocket);
                });

            } while (true);
        }

        public static void DoClient(TcpClient socket)
        {
            Stream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string message = sr.ReadLine();
            string answer = "";

            while (message != null && message != "")
            {
                Console.WriteLine("GET ALL" + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();
            }

            ns.Close();
            socket.Close();

        }

    }
}


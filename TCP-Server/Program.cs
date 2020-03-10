using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace TCP_Server
{
    class Program
    {

        private static readonly List<Book.Book> books = new List<Book.Book>()
        {
            new Book.Book("Løvernes Konge","JK Rowling",100,"1234567891011"),
            new Book.Book("Harry Potter","Omar Masouk",200,"1234567891012")
        };


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
                string[] messageArray = message.Split(' ');
                string param = message.Substring(message.IndexOf(' ') + 1);
                string command = messageArray[0];

                switch (command)
                {
                    case "GetAll":
                        sw.WriteLine("Get all recieved");
                        sw.WriteLine(JsonConvert.SerializeObject(books));
                        break;

                    case "Get":
                        sw.WriteLine("Get request recieved - Isbn " + messageArray[1] + "requested");
                        sw.WriteLine(JsonConvert.SerializeObject(books.Find(id=>id.ISBN13 == param)));
                        break;

                    case "Save":
                        sw.WriteLine("Save recieved");
                        Book.Book saveBook = JsonConvert.DeserializeObject<Book.Book>(param);
                        books.Add(saveBook);
                        break;

                    default:
                       sw.WriteLine("Den er gal");
                        break;
                }
                message = sr.ReadLine();

            }

            ns.Close();
            socket.Close();

        }

    }
}


using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace WhatIsThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var url = "https://www.google.com";
            Thread thread = new Thread(new ThreadStart(() => { PingWebPage100Times(url);}));
            thread.Start();
            Thread thread1 = new Thread(LogEvery5Seconds);
            thread1.Start();
        }

        private static void LogEvery5Seconds()
        {
            int i = 0;
            while (true)
            {
                Console.WriteLine($"Approximately {i * 5 } seconds elapsed since start of the program");
                Thread.Sleep(5000);
                i++;
            }
        }

        private static void PingWebPage100Times(string url)
        {
            HttpClient client = new HttpClient();
            for (int i = 0; i < 100; i++)
            {
                var responseMessage = client.GetAsync(url).Result;
                Console.WriteLine($"S.No. {i}: The response for https://google.com is {responseMessage.StatusCode}");
                Thread.Sleep(4000);
            }
            client.Dispose();
            
        }
    }
}

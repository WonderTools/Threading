using System;
using System.Net.Http;
using System.Threading;

namespace WhatIsBackgroundThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var url = "https://www.google.com";

            Thread thread1 = new Thread(LogEvery5Seconds);
            thread1.IsBackground = true;
            thread1.Start();

            Thread thread = new Thread(new ThreadStart(() => { PingWebPage10Times(url); }));
            thread.Start();
            
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

        private static void PingWebPage10Times(string url)
        {
            HttpClient client = new HttpClient();
            for (int i = 0; i < 10; i++)
            {
                var responseMessage = client.GetAsync(url).Result;
                Console.WriteLine($"S.No. {i}: The response for https://google.com is {responseMessage.StatusCode}");
                Thread.Sleep(4000);
            }
            client.Dispose();

        }
    }
}

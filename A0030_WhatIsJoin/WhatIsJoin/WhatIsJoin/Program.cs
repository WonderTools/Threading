using System;
using System.Net.Http;
using System.Threading;

namespace WhatIsJoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var url = "https://www.google.com";


            Thread thread = new Thread(new ThreadStart(() => { PingWebPage5Times(url); }));
            thread.Start();
            thread.Join();

            Thread thread1 = new Thread(LogEvery5Seconds);
            thread1.Start();


        }

        private static void LogEvery5Seconds()
        {
            int i = 0;
            while (true)
            {
                Console.WriteLine($"Approximately {i * 5 } seconds elapsed since start of the thread");
                Thread.Sleep(5000);
                i++;
                if(i > 4) break;
            }
        }

        private static void PingWebPage5Times(string url)
        {
            HttpClient client = new HttpClient();
            for (int i = 0; i < 5; i++)
            {
                var responseMessage = client.GetAsync(url).Result;
                Console.WriteLine($"S.No. {i}: The response for https://google.com is {responseMessage.StatusCode}");
                Thread.Sleep(4000);
            }
            client.Dispose();

        }
    }
}

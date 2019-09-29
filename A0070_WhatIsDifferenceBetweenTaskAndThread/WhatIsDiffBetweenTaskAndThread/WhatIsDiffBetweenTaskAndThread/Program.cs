using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace WhatIsDiffBetweenTaskAndThread
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().Wait();
        }

        private static async Task AsyncMain()
        {
            var urls = new List<string>()
            {
                "https://non-1.com",
                "https://non-2.com",
                "https://non-3.com",
                "https://non-4.com",
                "https://non-5.com",
                "https://non-6.com",
                "https://non-7.com",
                "https://non-8.com",
                "https://non-9.com",
                "https://non-10.com",
            };

            var tasks = urls.Select(url => Task.Run(() => { PingAWebPageNTimes(url); })).ToList();
            foreach (var task in tasks)
            {
                await task;
            }

            var newTasks = urls.Select(url => Task.Run(() => { PingAWebPageNTimes(url); })).ToList();
            foreach (var task in newTasks)
            {
                await task;
            }
        }

        static void PingAWebPageNTimes(string s)
        {
            for (int i = 0; i < 10; i++)
            {
                HttpClient client = new HttpClient();
                try
                {
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine($"ThreadId:{threadId},S.No:{i},url:{s},status:Initiated");
                    var x = client.GetAsync(s).Result;
                }
                catch (Exception e)
                {
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine($"ThreadId:{threadId},S.No:{i},url:{s},status:Completed");
                }
                client.Dispose();
            }
        }
    }
}

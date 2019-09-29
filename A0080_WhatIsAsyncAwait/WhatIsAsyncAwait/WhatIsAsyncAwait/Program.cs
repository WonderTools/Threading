using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WhatIsAsyncAwait
{
    class Program
    {
        static void Main(string[] args)
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

            //Run one of the experiments at a time
            TaskExperiment(urls);
            
        }

        private static void TaskExperiment(List<string> urls)
        {
            RunAllTasksToFinish(urls);
            RunAllTasksToFinish(urls);
        }

        private static void RunAllTasksToFinish(List<string> urls)
        {
            var tasks = urls.Select(url => Task.Run(async () => { await PingAWebPageNTimes(url); })).ToList();
            Task.WaitAll(tasks.ToArray());
        }

        static async Task PingAWebPageNTimes(string s)
        {
            for (int i = 0; i < 10; i++)
            {
                var threadId1 = 0;
                var threadId2 = 0;
                HttpClient client = new HttpClient();
                try
                {
                    threadId1 = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine($"ThreadId:{threadId1},S.No:{i},url:{s},status:Initiated");

                    //await client.GetAsync(s);
                    var x = client.GetAsync(s).Result;
                }
                catch (Exception e)
                {
                    threadId2 = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine($"ThreadId:{threadId2},S.No:{i},url:{s},status:Completed");
                    Console.WriteLine(threadId1 == threadId2 ? "The Thread id is same" : "The thread Id is different");
                }
                

                client.Dispose();
            }
        }
    }
}

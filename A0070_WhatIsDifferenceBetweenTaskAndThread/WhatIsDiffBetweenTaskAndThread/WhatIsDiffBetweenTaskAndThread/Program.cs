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
            //TaskExperiment(urls);
            ThreadExperiment(urls);
        }

        private static void TaskExperiment(List<string> urls)
        {
            RunAllTasksToFinish(urls);
            RunAllTasksToFinish(urls);
        }

        private static void RunAllTasksToFinish(List<string> urls)
        {
            var tasks = urls.Select(url => Task.Run(() => { PingAWebPageNTimes(url); })).ToList();
            Task.WaitAll(tasks.ToArray());
        }

        private static void ThreadExperiment(List<string> urls)
        {
            RullAllThreadsToFinish(urls);
            RullAllThreadsToFinish(urls);
        }

        private static void RullAllThreadsToFinish(List<string> urls)
        {
            var threads = urls.Select(url =>
            {
                Thread t = new Thread(() => PingAWebPageNTimes(url));
                t.Start();
                return t;
            }).ToList();
            foreach (var thread in threads)
            {
                thread.Join();
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

using System;
using System.Net.Http;
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
            var t1 = Task.Run(async () => { await PingAWebPage100Times("https://non-1.com"); });
            var t2 = Task.Run(async () => { await PingAWebPage100Times("https://non-2.com"); });
            var t3 = Task.Run(async () => { await PingAWebPage100Times("https://non-3.com"); });
            var t4 = Task.Run(async () => { await PingAWebPage100Times("https://non-4.com"); });
            var t5 = Task.Run(async () => { await PingAWebPage100Times("https://non-5.com"); });
            var t6 = Task.Run(async () => { await PingAWebPage100Times("https://non-6.com"); });
            var t7 = Task.Run(async () => { await PingAWebPage100Times("https://non-7.com"); });
            var t8 = Task.Run(async () => { await PingAWebPage100Times("https://non-8.com"); });
            var t9 = Task.Run(async () => { await PingAWebPage100Times("https://non-9.com"); });
            var t10 = Task.Run(async () => { await PingAWebPage100Times("https://non-10.com"); });

            await t1;
            await t2;
            await t3;
            await t4;
            await t5;
            await t6;
            await t7;
            await t8;
            await t9;
            await t10;

        }

        static async Task PingAWebPage100Times(string s)
        {
            for (int i = 0; i < 100; i++)
            {
                HttpClient client = new HttpClient();
                try
                {
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine($"ThreadId:{threadId},S.No:{i},url:{s},status:Initiated");
                    //var x = client.GetAsync(s).Result;
                    await client.GetAsync(s);
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

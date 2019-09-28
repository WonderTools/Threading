using System;
using System.Collections.Generic;
using System.Threading;

namespace WhatIsMonitor
{
    class Program
    {
        private const int QueueLimit = 4;

        static Queue<String> _stockHolder = new Queue<String>(QueueLimit);
        static Semaphore _semaphoreForQueueItem;
        static Semaphore _semaphoreForQueueSlot;

        static void Main(string[] args)
        {
            Test();
        }

        private static void Test()
        {
            _semaphoreForQueueItem = new Semaphore(0, QueueLimit);
            _semaphoreForQueueSlot = new Semaphore(QueueLimit, QueueLimit);
            Thread t1 = new Thread(Produce100Strings);
            t1.Start();
            Thread t2 = new Thread(ConsumeAndPrint100Strings);
            t2.Start();
            t1.Join();
            t2.Join();
            _semaphoreForQueueItem.Dispose();
            _semaphoreForQueueSlot.Dispose();
        }

        private static void ConsumeAndPrint100Strings()
        {
            for (int i = 0; i < 100; i++)
            {
                string s = GetString();
                Console.WriteLine($"S.No. {i} -> {s}");
            }
        }

        private static string GetString()
        {
            string result = String.Empty;
            _semaphoreForQueueItem.WaitOne();
            lock (_stockHolder)
            {
                result = _stockHolder.Dequeue();
            }
            _semaphoreForQueueSlot.Release();
            return result;
        }


        private static void Produce100Strings()
        {
            for (int i = 0; i < 100; i++)
            {
                Random random = new Random();
                var randomNumber = random.Next(1, 10);
                Console.WriteLine($"S.No. {i} Producing string with {randomNumber}");
                char[] charArray = new char[randomNumber];
                for (int j = 0; j < randomNumber; j++) charArray[j] = char.Parse(j.ToString());
                var produce = new String(charArray);
                Queue(produce);
            }
        }

        private static void Queue(string produce)
        {
            _semaphoreForQueueSlot.WaitOne();
            lock (_stockHolder)
            {
                _stockHolder.Enqueue(produce);
            }
            _semaphoreForQueueItem.Release();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace WhatIsSemaphore
{
    class Program
    {
        private const int QueueLimit = 4;

        static Queue<String> _stockHolder = new Queue<String>(QueueLimit);
        static Semaphore _semaphore = new Semaphore(0, QueueLimit);

        static void Main(string[] args)
        {
            Thread t1 = new Thread(Produce100Strings);
            t1.Start();
            Thread t2 = new Thread(ConsumeAndPrint100Strings);
            t2.Start();
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
            _semaphore.WaitOne();
            lock (_stockHolder)
            {
                return _stockHolder.Dequeue();
            }
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
            while (true)
            {
                try
                {
                    _semaphore.Release();
                    lock (_stockHolder)
                    {
                        _stockHolder.Enqueue(produce);
                    }
                    return;
                }
                catch (SemaphoreFullException e)
                {
                    Console.WriteLine("Queue is full. Waiting");
                }
            }
        }
    }
}

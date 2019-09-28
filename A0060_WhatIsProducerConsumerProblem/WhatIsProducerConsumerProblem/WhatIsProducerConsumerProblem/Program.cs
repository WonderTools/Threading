using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace WhatIsProducerConsumerProblem
{
    class Program
    {
        private const int QueueLimit = 4;

        static Queue<String> _stockHolder = new Queue<String>(9);
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Produce100Strings);
            t1.Start();
            Thread t2 = new Thread(ConsumeAndPrint100Strings);
            t2.Start();


        }

        private static void ConsumeAndPrint100Strings()
        {
            for(int i = 0; i < 100; )
            {
                string s = GetStringOrNull();
                if (s == null)
                {
                    Console.WriteLine("Waiting as the queue is empty");
                    Thread.Sleep(400);
                }
                else
                {
                    Console.WriteLine($"S.No. {i} -> {s}");
                    i++;
                }
            }
        }

        private static string GetStringOrNull()
        {
            lock (_stockHolder)
            {
                if (_stockHolder.Count == 0) return null;
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
                while (!TryQueue(produce))
                {
                    Console.WriteLine("Queue is full so waiting...");
                    Thread.Sleep(100);
                }
            }
        }

        private static bool TryQueue(string produce)
        {
            lock (_stockHolder)
            {
                if (_stockHolder.Count >= QueueLimit) return false;
                _stockHolder.Enqueue(produce);
                return true;
            }
        }
    }    
}

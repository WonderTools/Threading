using System;
using System.Collections.Generic;
using System.Threading;

namespace WhatIsMonitor
{
    class Program
    {
        private const int QueueLimit = 4;

        static Queue<String> _stockHolder = new Queue<String>(QueueLimit);

        static void Main(string[] args)
        {
            Test();
        }

        private static void Test()
        {
            Thread t1 = new Thread(Produce100Strings);
            t1.Start();
            Thread t2 = new Thread(ConsumeAndPrint100Strings);
            t2.Start();
            t1.Join();
            t2.Join();
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

            lock (_stockHolder)
            {
                if (_stockHolder.Count == 0) Monitor.Wait(_stockHolder);
                result = _stockHolder.Dequeue();
                Monitor.Pulse(_stockHolder);
            }
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
            lock (_stockHolder)
            {
                if (_stockHolder.Count >= QueueLimit) Monitor.Wait(_stockHolder);
                _stockHolder.Enqueue(produce);
                Monitor.Pulse(_stockHolder);
            }
        }
    }
}

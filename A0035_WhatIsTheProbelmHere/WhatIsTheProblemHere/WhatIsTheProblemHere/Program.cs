using System;
using System.Threading;

namespace WhatIsTheProblemHere
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => PrintUpTriangleNTimes(5, 10));
            Thread t2 = new Thread(() => PrintDownTriangleNTimes(5, 10));

            //Always keep one option commented
            //Option1(t1, t2);
            Option2(t1, t2);

        }

        private static void Option1(Thread t1, Thread t2)
        {
            t1.Start();
            t1.Join();
            t2.Start();
        }

        private static void Option2(Thread t1, Thread t2)
        {
            t1.Start();
            t2.Start();
        }

        private static void PrintDownTriangle(int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = i; j < length; j++)
                {
                    Console.Write("o");
                    MinorDelay();
                }

                Console.WriteLine();
            }
        }


        private static void PrintUpTriangleNTimes(int length, int n)
        {
            for(int i = 0; i < n ;i++) PrintUpTriangle(length);
        }

        private static void PrintDownTriangleNTimes(int length, int n)
        {
            for (int i = 0; i < n; i++) PrintDownTriangle(length);
        }

        private static void PrintUpTriangle(int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(".");
                    MinorDelay();
                }

                Console.WriteLine();
            }
        }

        private static void MinorDelay()
        {
            int j = 0;
            for (int i = 0; i < 10000; i++)
            {
                j++;
            }
        }
    }

    
}

using System;
using System.Threading;

namespace WhatIsTheProblemHere
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => PrintDownTriangle(5));
            Thread t2 = new Thread(() => PrintUpTriangle(5));

            //Comment one of the options
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
                    Thread.Sleep(400);
                }

                Console.WriteLine();
            }
        }


        private static void PrintUpTriangle(int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(".");
                    Thread.Sleep(700);
                }

                Console.WriteLine();
            }
        }
    }
}

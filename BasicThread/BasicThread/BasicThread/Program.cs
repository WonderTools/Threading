using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicThread
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread thread = new Thread(() => { Print("s");});
            thread.Name = "thread 1";
            thread.IsBackground = true;

            Thread thread2 = new Thread(() => { Print("*************"); });
            //thread2.Name = "thread 2";
            thread2.IsBackground = true;


            thread.Start();

            thread2.Start();

            Print("Good");

            //thread2.Join();
            //thread.Join();

        }

        static void Print(string s)
        {
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"Printing {i} -> {s} -> {Thread.CurrentThread.ManagedThreadId}");
                //Thread.Sleep(300);
            }
        }
    }
}

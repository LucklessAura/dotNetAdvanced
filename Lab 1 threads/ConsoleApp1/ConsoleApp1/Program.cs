using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;



namespace ConsoleApp1
{
    class Program
    {
        static int numar;
        static List<string> progress = new List<string>();
        static void Main(string[] args)
        {
            numar = 1000;
            ThreadStart threadOneReference = new ThreadStart(Metoda1);
            Thread threadOne = new Thread(threadOneReference);
            threadOne.Name = "Thread 1";
            ThreadStart threadTwoReference = new ThreadStart(Metoda2);
            Thread threadTwo = new Thread(threadTwoReference);
            threadTwo.Name = "Thread 2";

            threadOne.Start();

            threadTwo.Start();

            threadOne.Join();
            threadTwo.Join();

            progress.ForEach(el => { Console.WriteLine(el); });

            progress = new List<string>();
            BackgroundWorker worker1 = new BackgroundWorker();
            BackgroundWorker worker2 = new BackgroundWorker();


            worker1.DoWork += doWork1;
            worker2.DoWork += doWork2;

            

            worker1.RunWorkerAsync();
            worker2.RunWorkerAsync();

            while(worker1.IsBusy || worker2.IsBusy)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("\n\n\n");
            progress.ForEach(el => { Console.WriteLine(el); });
        }

        private static void doWork2(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Background Worker 2";
            Metoda2();
        }

        private static void doWork1(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Background Worker 1";
            Metoda1();
        }

        public static void Metoda1()
        {
            string startInfo = "Start fir: " + " nume thread: " + Thread.CurrentThread.Name + " " + DateTime.Now.ToString("yyyyMMddHHmmssffff") + " numar dat:" + numar.ToString();
            progress.Add(startInfo);
            string temporayExitInfo = "Iesire temporara fir: " + "  nume thread: " + Thread.CurrentThread.Name + " " + DateTime.Now.ToString("yyyyMMddHHmmssffff") + " numar dat:" + numar.ToString();
            int found = -1;
            for (int i = numar - 1; i > 1; i--)
            {
                bool f = true;
                for(int j= 2;j<Math.Sqrt(i);j++)
                {
                    if(i % j == 0)
                    {
                        f = false;
                    }
                }
                if(f == true)
                {
                    found = i;
                    break;
                }
            }
            string endInfo = "End fir: " + "  nume thread: " + Thread.CurrentThread.Name + " " + DateTime.Now.ToString("yyyyMMddHHmmssffff") + " numar prim:" + found;
            progress.Add(endInfo);
        }
        public static void Metoda2()
        {
            string startInfo = "Start fir: " + " nume thread: " + Thread.CurrentThread.Name + " " + DateTime.Now.ToString("yyyyMMddHHmmssffff") + " numar dat:" + numar.ToString();
            progress.Add(startInfo);
            string temporayExitInfo = "Iesire temporara fir: " + "  nume thread: " + Thread.CurrentThread.Name + " " + DateTime.Now.ToString("yyyyMMddHHmmssffff") + " numar dat:" + numar.ToString();
            int found = -1;
            for (int i = 2; i < numar; i++)
            {
                bool f = true;
                for (int j = 2; j < Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        f = false;
                    }
                }
                if (f == true)
                {
                    found = i;
                }
            }
            string endInfo = "End fir: " + "  nume thread: " + Thread.CurrentThread.Name + " " + DateTime.Now.ToString("yyyyMMddHHmmssffff") + " numar prim:" + found;
            progress.Add(endInfo);
        }
    }
}

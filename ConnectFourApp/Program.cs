using System;
using System.Timers;
using ConnectFour.Display;

namespace ConnectFourApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer RewriteTimer;
            RewriteTimer = new Timer();
            RewriteTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            RewriteTimer.Interval = 100;
            RewriteTimer.Enabled = true;
            ConsoleKeyInfo keyInfo;
            Display output = Display.GetInstance();

            while (Console.ReadKey().Key != ConsoleKey.Enter) {
                keyInfo = Console.ReadKey(true);
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e) {
            Display output = Display.GetInstance();
            output.DisplayText();
        }
    }
}

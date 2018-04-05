using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaWorkshop
{
    public class PrinterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is string msg)
            {
                if (msg.Contains("Two"))
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (msg.Contains("One"))
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(msg);
                Console.ResetColor();
            }
        }
    }
}

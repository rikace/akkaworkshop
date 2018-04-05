using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaWorkshop
{
    class Program
    {
        public static ActorSystem ActorSystem;

        static void Main(string[] args)
        {

            using (ActorSystem = ActorSystem.Create("ActorSystem"))
            {
                var processValidationActor = ActorSystem.ActorOf<ProcessValidationActor>("ProcessValidation");

                string val = "1";
                do
                {
                    Console.WriteLine("Insert a valid positive number and press ENTER to start");
                    val = Console.ReadLine();

                    if (int.TryParse(val, out int numberOfIndividuals))
                        processValidationActor.Tell(ProcessValidationStart.Create(numberOfIndividuals));

                } while (val != "q");
            }
        }
    }
}

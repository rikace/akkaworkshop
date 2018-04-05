using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaWorkshop
{
    public class AggregatorActor : ReceiveActor
    {
        public static string Name => "Aggregator";

        // STEP 7
        // private readonly collection-type
        private DateTime timeCompletedValidation = DateTime.Now;

        public AggregatorActor(string[] rules, int individualIdCount)
        {
            // collection-type = map rules to list of individuals, or an individuals counter

            Receive<RuleResult>(msg =>
            {
                // STEP 7
                // Create a local storage/state (could be a List<>, or HashSet<> or ...)
                // to maintain the state of the rules completed.
                // Notes : the incoming message "RuleResult" contains the name of the rule validated and the related individual id.
                //         When all the individual for a rule have been validated, that rule can be flagged as completed

                // STEP 8
                // When all the rules are completed (flagged), the actor should calculate the total execution time "totalTime"
                // and than send the value to the "ProcessValidationActor" actor using the "ProcessValidationCompleted" message.
                //
                // Notes :  to access the "ProcessValidationActor" from here, you might consider to send the messages from the rule actors
                //          as Sender, then you can access the correct IActorRef here
                //          Alternatively, investigate and exploit the "ActorSelection" to send a message directly to an Actor address (ActorPath)
                //          "Context.ActorSelection"   (Bonus 5)
                var totalTime = DateTime.Now.Subtract(timeCompletedValidation);

            });
        }
        public static Props Create(string[] rules, int individualIdCount) => Props.Create(() => new AggregatorActor(rules, individualIdCount));

    }
}

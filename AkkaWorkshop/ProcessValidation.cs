using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AkkaWorkshop
{
    public class ProcessValidationActor : ReceiveActor
    {
        private IActorRef[] children;
        private IActorRef printer;
        protected override void PreStart()
        {
            printer = Context.ActorOf(Props.Create(() => new PrinterActor()),
          "printerActor");

            // STEP 1
            // TO DO
            // add the code to create the two rule Actors (RuleOneActor, RuleTwoActor)
            // Notes : maintain the argument constructor
            // Notes : Both the rule Actors have static method helper (Create) for the instantiation of a new Actor
            // Notes : important to give a Name to the each Actor
            // Notes : When validation process works, try to investigate/add a "Round-Robin-Pool" router (WithRouter) to increase the degree of parallelism (Bonus point 5)
            children = new IActorRef[]
                  {

                  };
        }


        public ProcessValidationActor()
        {
            Receive<ProcessValidationStart>(p =>
            {
                var individualIds = Enumerable.Range(1, p.NumberOfIndividuals).ToList();

                // STEP 2
                // Complete the GenerateAggregator
                IActorRef aggregator = GenerateAggregator(individualIds.Count);

                // STEP 3
                // broadcast (send the messages) of all the individualIds (one individual at a time)
                // to all the children Actors
                // Notes : use the "StartProcessRule" message.
                //         In the payload of the "Tell" method you can pass an extra argument that defines the "Sender" actor
                //         This argument could be used by the receiver Actor to reply and/or send a message to a different Actor
                //         Because the output of the each rule actor should be sent to Aggregator actor, you might need to use the ... as "Sender"
                //
                //         Otherwise you could add the ActorRef of the Aggregator Actor in the payload of the StartProcessRule
                // Notes : you could use a for-loop or investigate the AKka.NET built in "Broadcast" message (Bonus point 5)

            });

            Receive<ProcessValidationCompleted>(p =>
            {
                printer.Tell($"Validation completed in { p.EndTime } ");
            });
        }

        private IActorRef GenerateAggregator(int individualIdCount)
        {
            var validationId = Guid.NewGuid();
            var aggrId = $"{AggregatorActor.Name}-{validationId}";

            // Checks if the Actor with the same name already exists,
            // and a new actor is created accordingly
            var aggregator = Context.Child(aggrId);
            if (Equals(aggregator, ActorRefs.Nobody))
            {
                // STEP 1
                // add the code to create a new AggregatorActor
                // Notes : Use the in "Context" to add the new actor to the current ProcessValidationActor
                // to generate a child-parent relationship.
                // Notes : satisfy the arguments the AggregatorActor contractor arguments
                aggregator = null;
            }
            return aggregator;
        }
    }

}

using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaWorkshop
{
    public interface IValidate
    {
        Task<ValidationResult> Validate(int individualId);
    }

    public class RuleOneActor : ReceiveActor, IValidate
    {
        private Random rnd = new Random(Guid.NewGuid().GetHashCode());
        public static string Name => "RuleOne";
        private readonly IActorRef printerActor;
        public RuleOneActor(IActorRef printerActor)
        {
            this.printerActor = printerActor;

            Receive<StartProcessRule>(msg =>
            {
                // STEP 4
                // validate the "IndividualId" part of the message (StartProcessRule) payload
                // Notes : use the "Validate" method, be aware that this is asynchronous ...
                ValidationResult result = null; // replace the null with the code implementation "...Validate(..."

                var ruleResult = RuleResult.Create(Name, result);

                // STEP 5
                // Send the "ruleResult" message to the Aggregate actor,
            });
        }



        public static Props Create(IActorRef printerActor) => Props.Create(() => new RuleOneActor(printerActor));

        public async Task<ValidationResult> Validate(int individualId)
        {
            await Task.Delay(rnd.Next(10, 1000));
            var validationresult = ValidationResult.Create(individualId, rnd.Next() % 2 == 0);
            printerActor.Tell($"Individual {individualId} for rule {Name} is {validationresult.IsValid}");
            return validationresult;
        }
    }

    public class RuleTwoActor : ReceiveActor, IValidate
    {
        private Random rnd = new Random(Guid.NewGuid().GetHashCode());
        public static string Name => "RuleTwo";

        private readonly IActorRef printerActor;

        public RuleTwoActor(IActorRef printerActor)
        {
            this.printerActor = printerActor;

            Receive<StartProcessRule>(msg =>
            {
                // STEP 6
                // Same as Step 5 but investigate for extra bonus points a different way to handle asynchronous computations
                // inside a "Receive" function   (Bonus 5)
            });
        }

        public static Props Create(IActorRef printerActor) => Props.Create(() => new RuleTwoActor(printerActor));

        public async Task<ValidationResult> Validate(int individualId)
        {
            await Task.Delay(1000);
            var validationresult = ValidationResult.Create(individualId, rnd.Next() % 5 == 0);
            printerActor.Tell($"Individual {individualId} for rule {Name} is {validationresult.IsValid}");
            return validationresult;
        }
    }

}

using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaWorkshop
{
    public class ProcessValidationStart
    {
        public int NumberOfIndividuals { get; }
        private ProcessValidationStart(int numberOfIndividuals) { this.NumberOfIndividuals = numberOfIndividuals; }
        public static ProcessValidationStart Create(int numberOfIndividuals) => new ProcessValidationStart(numberOfIndividuals);
    }

    public class ProcessValidationCompleted
    {
        private ProcessValidationCompleted(TimeSpan endTime)
        {
            this.EndTime = endTime;
        }
        public TimeSpan EndTime { get;}
        public static ProcessValidationCompleted Create(TimeSpan endTime) => new ProcessValidationCompleted(endTime);
    }

    public class StartProcessRule
    {
        public int IndividualId { get; }

        public IActorRef ActorRefNext { get;  }

        private StartProcessRule(int id, IActorRef actorRef)
        {
            this.IndividualId = id;
            this.ActorRefNext = actorRef;
        }

        public static StartProcessRule Create(int id, IActorRef actorRef) => new StartProcessRule(id, actorRef);
    }

    public class RuleResult
    {
        public string RuleName { get; }
        public ValidationResult ValidationResult { get;  }

        private RuleResult(string ruleName, ValidationResult validationResult)
        {
            this.RuleName = ruleName;
            this.ValidationResult = validationResult;
        }

        public static RuleResult Create(string ruleName, ValidationResult validationResult) => new RuleResult(ruleName, validationResult);
    }

    public class ValidationResult
    {
        public int IndividualId { get;}

        public bool IsValid { get; }

        private ValidationResult(int id, bool isValid)
        {
            this.IndividualId = id;
            this.IsValid = isValid;
        }

        public static ValidationResult Create(int id, bool isValid) => new ValidationResult(id, isValid);
    }

}

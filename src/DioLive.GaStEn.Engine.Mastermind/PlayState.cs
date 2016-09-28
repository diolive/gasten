using System;
using System.Collections.Generic;
using System.Linq;

namespace DioLive.GaStEn.Engine.Mastermind
{
    public class PlayState : State
    {
        private static readonly Random Rnd = new Random();

        private readonly string target;

        public PlayState(int length)
            : base((int)States.Play)
        {
            if (length < 1 || length > 10)
            {
                throw new ArgumentException("Length should be between 1 and 10", nameof(length));
            }

            List<int> digits = new List<int>(Enumerable.Range(0, 10));
            char[] mixed = new char[length];
            for (int i = 0; i < length; i++)
            {
                var index = Rnd.Next(digits.Count);
                mixed[i] = digits[index].ToString()[0];
                digits.RemoveAt(index);
            }

            this.target = new string(mixed);
        }

        protected override ProcessResult ProcessMessage(Message message)
        {
            switch ((Messages)message.MessageId)
            {
                case Messages.Test:
                    return this.Process((TestMessage)message);

                default:
                    return ProcessResult.NotSupported();
            }
        }

        private ProcessResult Process(TestMessage message)
        {
            var result = this.Test(message.Assumption);
            if (result.Success)
            {
                if (result.Bulls < message.Assumption.Length)
                {
                    return ProcessResult.Ok(this, $"Bulls: {result.Bulls}, cows: {result.Cows}");
                }
                else
                {
                    return ProcessResult.Ok(new WinState(), $"You are right, it was {message.Assumption}!");
                }
            }
            else
            {
                return ProcessResult.NoAction("Wrong assumption");
            }
        }

        private TestResult Test(string assumption)
        {
            if (assumption.Distinct().Count() != assumption.Length)
            {
                return TestResult.Failed;
            }

            if (assumption.Length != this.target.Length)
            {
                return TestResult.Failed;
            }

            int bulls = 0;
            int cows = 0;

            for (int i = 0; i < assumption.Length; i++)
            {
                if (assumption[i] == this.target[i])
                {
                    bulls++;
                }
                else if (this.target.Contains(assumption[i]))
                {
                    cows++;
                }
            }

            return new TestResult(bulls, cows);
        }
    }
}
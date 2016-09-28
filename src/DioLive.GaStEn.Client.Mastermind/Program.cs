using System;

using DioLive.GaStEn.Engine.Mastermind;

namespace DioLive.GaStEn.Client.Mastermind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MastermindStateMachine engine = new MastermindStateMachine();

            do
            {
                Console.Write("Enter your assumption: ");
                string assumption = Console.ReadLine();

                var result = engine.Test(assumption);
                if (result.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(result.StatusMessage);
                Console.ResetColor();
            }
            while (engine.CurrentStateId != (int)States.Win);
        }
    }
}
namespace DioLive.GaStEn.Engine.Mastermind
{
    public class MastermindStateMachine : StateMachine
    {
        public MastermindStateMachine(int length = 4)
           : base(new PlayState(length))
        {
        }

        public ProcessResult Test(string assumption)
        {
            return this.ProcessMessage(new TestMessage { Assumption = assumption });
        }
    }
}
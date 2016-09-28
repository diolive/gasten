using System;

namespace DioLive.GaStEn.Engine
{
    public class StateMachine
    {
        public StateMachine(State initialState)
        {
            this.CurrentState = initialState;
        }

        public State CurrentState { get; private set; }

        public int CurrentStateId => this.CurrentState.StateId;

        public ProcessResult ProcessMessage(Message message)
        {
            ProcessResult result = this.CurrentState.ProcessMessage(message);

            switch (result.Result)
            {
                case ProcessResults.Success:
                    this.CurrentState = result.State;
                    break;

                case ProcessResults.NoAction:
                    break;

                case ProcessResults.Failed:
                    throw new ProcessException(result.StatusMessage);

                default:
                    throw new InvalidOperationException("Unknown result: " + result.Result);
            }

            return result;
        }
    }
}
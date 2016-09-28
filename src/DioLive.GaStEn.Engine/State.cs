namespace DioLive.GaStEn.Engine
{
    public abstract class State
    {
        protected State(int stateId)
        {
            this.StateId = stateId;
        }

        public int StateId { get; }

        protected internal abstract ProcessResult ProcessMessage(Message message);
    }
}
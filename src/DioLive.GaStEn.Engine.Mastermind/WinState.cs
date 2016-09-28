namespace DioLive.GaStEn.Engine.Mastermind
{
    public class WinState : State
    {
        public WinState()
            : base((int)States.Win)
        {
        }
         
        protected override ProcessResult ProcessMessage(Message message)
            => ProcessResult.NoAction("Game is over, you are win");
    }
}
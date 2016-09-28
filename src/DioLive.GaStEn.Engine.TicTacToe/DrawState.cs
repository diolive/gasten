namespace DioLive.GaStEn.Engine.TicTacToe
{
    public class DrawState : BaseState
    {
        public DrawState(byte[,] field)
            : base(States.Draw, field)
        {
        }

        protected override ProcessResult ProcessMessage(Message message)
            => ProcessResult.NoAction("Draw: there is no more moves");
    }
}
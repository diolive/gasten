using DioLive.GaStEn.Engine;

namespace DioLive.TicTacToe.Engine
{
    public class WinState : BaseState
    {
        private byte currentPlayer;

        public WinState(byte currentPlayer, byte[,] field, Point[] solution)
            : base(States.Win, field)
        {
            this.currentPlayer = currentPlayer;
            this.Solution = solution;
        }

        public Point[] Solution { get; private set; }

        protected override ProcessResult ProcessMessage(Message message)
            => ProcessResult.NoAction("Win: current game is over");
    }
}
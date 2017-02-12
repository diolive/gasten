using DioLive.GaStEn.Engine;

namespace DioLive.TicTacToe.Engine
{
    public class TicTacToeStateMachine : StateMachine
    {
        public TicTacToeStateMachine()
            : this('X', '0')
        {
        }

        public TicTacToeStateMachine(char firstPlayer, char secondPlayer)
            : base(new PlayState(firstPlayer, secondPlayer))
        {
        }

        public ProcessResult Mark(char userChar, byte x, byte y)
        {
            return this.ProcessMessage(new SetMarkMessage { UserChar = userChar, X = x, Y = y });
        }
    }
}
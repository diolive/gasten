using System;
using System.Linq;

namespace DioLive.GaStEn.Engine.TicTacToe
{
    public class PlayState : BaseState
    {
        private byte currentPlayer;
        private char firstPlayer;
        private char secondPlayer;

        public PlayState(char firstPlayer, char secondPlayer)
            : base(States.Play, fieldSize: 3)
        {
            if (firstPlayer == secondPlayer)
            {
                throw new ArgumentException("First and second player should not be equals", nameof(secondPlayer));
            }

            this.currentPlayer = 1;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
        }

        protected override ProcessResult ProcessMessage(Message message)
        {
            switch ((Messages)message.MessageId)
            {
                case Messages.SetMark:
                    return this.Process((SetMarkMessage)message);

                default:
                    return ProcessResult.NotSupported();
            }
        }

        private ProcessResult Process(SetMarkMessage setMarkMessage)
        {
            byte playerIndex;
            if (setMarkMessage.UserChar == this.firstPlayer)
            {
                playerIndex = 1;
            }
            else if (setMarkMessage.UserChar == this.secondPlayer)
            {
                playerIndex = 2;
            }
            else
            {
                return ProcessResult.Failed("Unknown player: " + setMarkMessage.UserChar);
            }

            if (playerIndex != this.currentPlayer)
            {
                return ProcessResult.NoAction("It's another player's move");
            }

            if (!this.TrySetMark(setMarkMessage.X, setMarkMessage.Y, playerIndex))
            {
                return ProcessResult.NoAction("This cell is filled already");
            }

            Point[] solution = this.FindSolutionFor(playerIndex);
            if (solution != null)
            {
                return ProcessResult.Ok(new WinState(playerIndex, this.Field, solution));
            }

            if (!this.IsMovesLeft)
            {
                return ProcessResult.Ok(new DrawState(this.Field));
            }

            this.SwitchPlayer();
            return ProcessResult.Ok(this);
        }

        private Point[] FindSolutionFor(byte currentPlayer)
        {
            byte[,] field = this.Field;
            int fieldSize = this.FieldSize;

            Point[] solution;
            for (byte i = 0; i < fieldSize; i++)
            {
                solution = this.CheckRow(field, i) ?? this.CheckColumn(field, i);
                if (solution != null)
                {
                    return solution;
                }
            }

            return this.CheckMainDiag(field) ?? this.CheckAltDiag(field);
        }

        private Point[] CheckRow(byte[,] field, byte row)
        {
            if (field[row, 0] == default(byte))
            {
                return null;
            }

            for (int column = 1; column < this.FieldSize; column++)
            {
                if (field[row, column] != field[row, 0])
                {
                    return null;
                }
            }

            return Enumerable.Range(0, this.FieldSize)
                .Select(column => new Point(row, (byte)column))
                .ToArray();
        }

        private Point[] CheckColumn(byte[,] field, byte column)
        {
            if (field[0, column] == default(byte))
            {
                return null;
            }

            for (int row = 1; row < this.FieldSize; row++)
            {
                if (field[row, column] != field[0, column])
                {
                    return null;
                }
            }

            return Enumerable.Range(0, this.FieldSize)
                .Select(row => new Point((byte)row, column))
                .ToArray();
        }

        private Point[] CheckMainDiag(byte[,] field)
        {
            if (field[0, 0] == default(byte))
            {
                return null;
            }

            for (int i = 1; i < this.FieldSize; i++)
            {
                if (field[i, i] != field[0, 0])
                {
                    return null;
                }
            }

            return Enumerable.Range(0, this.FieldSize)
                .Select(i => new Point((byte)i, (byte)i))
                .ToArray();
        }

        private Point[] CheckAltDiag(byte[,] field)
        {
            int maxIndex = this.FieldSize - 1;

            if (field[0, maxIndex] == default(byte))
            {
                return null;
            }

            for (int i = 1; i <= maxIndex; i++)
            {
                if (field[i, maxIndex - i] != field[0, maxIndex])
                {
                    return null;
                }
            }

            return Enumerable.Range(0, this.FieldSize)
                .Select(i => new Point((byte)i, (byte)(maxIndex - i)))
                .ToArray();
        }

        private void SwitchPlayer()
        {
            this.currentPlayer = (byte)(3 - this.currentPlayer);
        }
    }
}
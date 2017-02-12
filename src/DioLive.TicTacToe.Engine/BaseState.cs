using System;
using System.Linq;

using DioLive.GaStEn.Engine;

namespace DioLive.TicTacToe.Engine
{
    public abstract class BaseState : State
    {
        private int fieldSize;
        private byte[,] field;
        private int movesLeft;

        protected BaseState(States state, int fieldSize)
            : base((int)state)
        {
            if (fieldSize < 2)
            {
                throw new ArgumentException("Field size could not be less than 2", nameof(fieldSize));
            }

            this.fieldSize = fieldSize;
            this.field = new byte[fieldSize, fieldSize];
            this.movesLeft = fieldSize * fieldSize;
        }

        protected BaseState(States state, byte[,] field)
            : base((int)state)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            this.fieldSize = field.GetLength(0);

            if (this.fieldSize != field.GetLength(1))
            {
                throw new ArgumentException("Field should be squared", nameof(field));
            }

            if (this.fieldSize < 2)
            {
                throw new ArgumentException("Field size could not be less than 2", nameof(field));
            }

            this.movesLeft = field.Cast<byte>().Count(f => f != default(byte));

            this.field = field;
        }

        public bool IsMovesLeft => this.movesLeft > 0;

        public int FieldSize => this.fieldSize;

        protected byte[,] Field => this.field;

        public byte[,] GetField()
        {
            return (byte[,])this.field.Clone();
        }

        internal bool TrySetMark(int x, int y, byte player)
        {
            if (!this.IsEmpty(x, y))
            {
                return false;
            }

            this.SetMark(x, y, player);
            this.movesLeft--;
            return true;
        }

        private bool IsEmpty(int x, int y)
        {
            return this.field[x, y] == default(byte);
        }

        private void SetMark(int x, int y, byte player)
        {
            this.field[x, y] = player;
        }
    }
}
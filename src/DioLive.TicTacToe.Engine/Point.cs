namespace DioLive.TicTacToe.Engine
{
    public class Point
    {
        public Point()
        {
        }

        public Point(byte x, byte y)
        {
            this.X = x;
            this.Y = y;
        }

        public byte X { get; set; }

        public byte Y { get; set; }
    }
}
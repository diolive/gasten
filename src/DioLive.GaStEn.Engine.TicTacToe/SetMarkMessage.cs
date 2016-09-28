namespace DioLive.GaStEn.Engine.TicTacToe
{
    public class SetMarkMessage : Message
    {
        public SetMarkMessage()
            : base((int)Messages.SetMark)
        {
        }

        public char UserChar { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
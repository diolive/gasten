namespace DioLive.GaStEn.Engine.Mastermind
{
    public class TestMessage : Message
    {
        public TestMessage()
            : base((int)Messages.Test)
        {
        }

        public string Assumption { get; set; }
    }
}
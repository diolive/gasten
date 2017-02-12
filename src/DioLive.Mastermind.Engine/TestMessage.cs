using DioLive.GaStEn.Engine;

namespace DioLive.Mastermind.Engine
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
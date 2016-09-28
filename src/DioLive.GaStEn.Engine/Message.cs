namespace DioLive.GaStEn.Engine
{
    public abstract class Message
    {
        protected Message(int messageId)
        {
            this.MessageId = messageId;
        }

        public int MessageId { get; }
    }
}
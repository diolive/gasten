namespace DioLive.Thing.Engine
{
    public class Card
    {
        public Card(int number, CardTypes type, int reqCount)
        {
            this.Number = number;
            this.Type = type;
            this.ReqCount = reqCount;
        }

        public int Number { get; }

        public CardTypes Type { get; }

        public int ReqCount { get; }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace DioLive.GaStEn.Engine.Thing
{
    public class PlayState : State
    {
        private Deck deck;
        private List<Card>[] players;

        public PlayState(int playerCount)
            : base((int)States.Play)
        {
            this.deck = new Deck(playerCount);
            this.players = Enumerable.Range(0, playerCount)
                .Select(_ => new List<Card>(Constants.HandSize + 1))
                .ToArray();

            for (int i = 0; i < Constants.HandSize; i++)
            {
                for (int j = 0; j < playerCount; j++)
                {
                    this.players[j].Add(this.deck.Take());
                }
            }
        }

        protected override ProcessResult ProcessMessage(Message message)
        {
            switch ((Messages)message.MessageId)
            {
                default:
                    return ProcessResult.NotSupported();
            }
        }
    }
}
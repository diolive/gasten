using System;
using System.Collections.Generic;
using System.Linq;

namespace DioLive.Thing.Engine
{
    public class Deck
    {
        private Queue<Card> talon;
        private List<Card> discard;

        public Deck(int playerCount)
        {
            if (playerCount < Constants.MinPlayersCount || playerCount > Constants.MaxPlayersCount)
            {
                throw new ArgumentException("Players count should be between " + Constants.MinPlayersCount + " and " + Constants.MaxPlayersCount, nameof(playerCount));
            }

            HashSet<Card> cards = new HashSet<Card>(GetAllCards().Where(c => c.ReqCount <= playerCount));
            Card thing = cards.Single(c => c.Type == CardTypes.Thing);
            cards.Remove(thing);

            List<Card> noPanic = cards.Where(c => c.Type != CardTypes.Infection && !c.Type.HasFlag(CardTypes.Panic)).ToList();

            int initialCount = Constants.HandSize * playerCount;
            List<Card> initial = new List<Card>(initialCount);
            initial.Add(thing);
            initial.AddRange(Deck.Shuffle(noPanic).Take(initialCount - 1));

            cards.ExceptWith(initial);

            this.talon = new Queue<Card>(initial.Concat(Deck.Shuffle(cards.ToList())));
            this.discard = new List<Card>();
        }

        public bool IsNextPanic => this.talon.Peek().Type.HasFlag(CardTypes.Panic);

        public Card Take()
        {
            var card = this.talon.Dequeue();

            if (this.talon.Count == 0)
            {
                this.talon = new Queue<Card>(Deck.Shuffle(this.discard));
            }

            return card;
        }

        public Card Take(Func<Card, bool> criterio)
        {
            while (true)
            {
                var card = this.Take();
                if (criterio(card))
                {
                    return card;
                }

                this.Discard(card);
            }
        }

        public Card TakeEvent() => this.Take(card => !card.Type.HasFlag(CardTypes.Panic));

        public void Discard(Card card) => this.discard.Add(card);

        public void Reshuffle()
        {
            var list = new List<Card>(this.talon.Concat(this.discard));
            this.discard.Clear();
            this.talon = new Queue<Card>(Deck.Shuffle(list));
        }

        private static IEnumerable<Card> GetAllCards()
        {
            yield return new Card(1, CardTypes.Thing, 4);

            yield return new Card(2, CardTypes.Infection, 4);
            yield return new Card(3, CardTypes.Infection, 4);
            yield return new Card(4, CardTypes.Infection, 4);
            yield return new Card(5, CardTypes.Infection, 4);
            yield return new Card(6, CardTypes.Infection, 4);
            yield return new Card(7, CardTypes.Infection, 4);
            yield return new Card(8, CardTypes.Infection, 4);
            yield return new Card(9, CardTypes.Infection, 4);
            yield return new Card(10, CardTypes.Infection, 6);
            yield return new Card(11, CardTypes.Infection, 7);
            yield return new Card(12, CardTypes.Infection, 7);
            yield return new Card(13, CardTypes.Infection, 7);
            yield return new Card(14, CardTypes.Infection, 8);
            yield return new Card(15, CardTypes.Infection, 9);
            yield return new Card(16, CardTypes.Infection, 9);
            yield return new Card(17, CardTypes.Infection, 10);
            yield return new Card(18, CardTypes.Infection, 10);
            yield return new Card(19, CardTypes.Infection, 11);
            yield return new Card(20, CardTypes.Infection, 11);
            yield return new Card(21, CardTypes.Infection, 11);

            yield return new Card(22, CardTypes.FlameThrower, 4);
            yield return new Card(23, CardTypes.FlameThrower, 4);
            yield return new Card(24, CardTypes.FlameThrower, 6);
            yield return new Card(25, CardTypes.FlameThrower, 9);
            yield return new Card(26, CardTypes.FlameThrower, 11);

            yield return new Card(27, CardTypes.Analysis, 5);
            yield return new Card(28, CardTypes.Analysis, 6);
            yield return new Card(29, CardTypes.Analysis, 9);

            yield return new Card(30, CardTypes.PickAxe, 4);
            yield return new Card(31, CardTypes.PickAxe, 9);

            yield return new Card(32, CardTypes.Suspicion, 4);
            yield return new Card(33, CardTypes.Suspicion, 4);
            yield return new Card(34, CardTypes.Suspicion, 4);
            yield return new Card(35, CardTypes.Suspicion, 4);
            yield return new Card(36, CardTypes.Suspicion, 7);
            yield return new Card(37, CardTypes.Suspicion, 8);
            yield return new Card(38, CardTypes.Suspicion, 9);
            yield return new Card(39, CardTypes.Suspicion, 10);

            yield return new Card(40, CardTypes.Whisky, 4);
            yield return new Card(41, CardTypes.Whisky, 6);
            yield return new Card(42, CardTypes.Whisky, 10);

            yield return new Card(43, CardTypes.Persistence, 4);
            yield return new Card(44, CardTypes.Persistence, 4);
            yield return new Card(45, CardTypes.Persistence, 6);
            yield return new Card(46, CardTypes.Persistence, 9);
            yield return new Card(47, CardTypes.Persistence, 10);

            yield return new Card(48, CardTypes.LookAround, 4);
            yield return new Card(49, CardTypes.LookAround, 9);

            yield return new Card(50, CardTypes.ChangePositions, 4);
            yield return new Card(51, CardTypes.ChangePositions, 4);
            yield return new Card(52, CardTypes.ChangePositions, 7);
            yield return new Card(53, CardTypes.ChangePositions, 9);
            yield return new Card(54, CardTypes.ChangePositions, 11);

            yield return new Card(55, CardTypes.ClearOut, 4);
            yield return new Card(56, CardTypes.ClearOut, 4);
            yield return new Card(57, CardTypes.ClearOut, 7);
            yield return new Card(58, CardTypes.ClearOut, 9);
            yield return new Card(59, CardTypes.ClearOut, 11);

            yield return new Card(60, CardTypes.Temptation, 4);
            yield return new Card(61, CardTypes.Temptation, 4);
            yield return new Card(62, CardTypes.Temptation, 6);
            yield return new Card(63, CardTypes.Temptation, 7);
            yield return new Card(64, CardTypes.Temptation, 8);
            yield return new Card(65, CardTypes.Temptation, 10);
            yield return new Card(66, CardTypes.Temptation, 11);

            yield return new Card(67, CardTypes.Fear, 5);
            yield return new Card(68, CardTypes.Fear, 6);
            yield return new Card(69, CardTypes.Fear, 8);
            yield return new Card(70, CardTypes.Fear, 11);

            yield return new Card(71, CardTypes.IWillStayHere, 4);
            yield return new Card(72, CardTypes.IWillStayHere, 6);
            yield return new Card(73, CardTypes.IWillStayHere, 11);

            yield return new Card(74, CardTypes.NoThanks, 4);
            yield return new Card(75, CardTypes.NoThanks, 6);
            yield return new Card(76, CardTypes.NoThanks, 8);
            yield return new Card(77, CardTypes.NoThanks, 11);

            yield return new Card(78, CardTypes.Miss, 4);
            yield return new Card(79, CardTypes.Miss, 6);
            yield return new Card(80, CardTypes.Miss, 11);

            yield return new Card(81, CardTypes.NoKebab, 4);
            yield return new Card(82, CardTypes.NoKebab, 6);
            yield return new Card(83, CardTypes.NoKebab, 11);

            yield return new Card(84, CardTypes.Quarantine, 5);
            yield return new Card(85, CardTypes.Quarantine, 9);

            yield return new Card(86, CardTypes.LockedDoor, 4);
            yield return new Card(87, CardTypes.LockedDoor, 7);
            yield return new Card(88, CardTypes.LockedDoor, 11);

            yield return new Card(89, CardTypes.OldRopes, 6);
            yield return new Card(90, CardTypes.OldRopes, 9);

            yield return new Card(91, CardTypes.OneTwo, 5);
            yield return new Card(92, CardTypes.OneTwo, 9);

            yield return new Card(93, CardTypes.ThreeFour, 4);
            yield return new Card(94, CardTypes.ThreeFour, 9);

            yield return new Card(95, CardTypes.BadParty, 5);
            yield return new Card(96, CardTypes.BadParty, 9);

            yield return new Card(97, CardTypes.GoAway, 5);

            yield return new Card(98, CardTypes.Forgetfulness, 4);

            yield return new Card(99, CardTypes.ChainReaction, 4);
            yield return new Card(100, CardTypes.ChainReaction, 9);

            yield return new Card(101, CardTypes.LetsBeFriends, 7);
            yield return new Card(102, CardTypes.LetsBeFriends, 9);

            yield return new Card(103, CardTypes.BlindDate, 4);
            yield return new Card(104, CardTypes.BlindDate, 9);

            yield return new Card(105, CardTypes.Oops, 10);

            yield return new Card(106, CardTypes.DontTellAnybody, 7);
            yield return new Card(107, CardTypes.DontTellAnybody, 9);

            yield return new Card(108, CardTypes.ConfesstionTime, 8);

            yield return new Card(201, CardTypes.Necronomicon, 10);

            yield return new Card(202, CardTypes.HPLovecraft, 6);
        }

        private static IEnumerable<Card> Shuffle(List<Card> cards)
        {
            Random rnd = RandomHelper.Instance;
            while (cards.Count > 0)
            {
                int index = rnd.Next(cards.Count);
                Card card = cards[index];
                cards.RemoveAt(index);
                yield return card;
            }
        }
    }
}
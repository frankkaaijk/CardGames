using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections;

[assembly: InternalsVisibleTo("TurnBasedCardGameTests")]
namespace TurnBasedCardGame
{
    internal class Deck : IEquatable<object>
    {
        internal static Random random = new Random();
        internal List<Card> Cards;

        public Deck()
        {
            Cards = new List<Card>();            
        }

        public void Initialize()
        {
            foreach (var suit in (Card.Suits[])Enum.GetValues(typeof(Card.Suits)))
            {
                if (suit != Card.Suits.Jokers)
                {
                    foreach (var value in (Card.Values[])Enum.GetValues(typeof(Card.Values)))
                    {
                        Cards.Add(new Card(suit, value));
                    }
                }
            }
            // Add 2 jokers
            Cards.Add(new Card(Card.Suits.Jokers, Card.Values.Ace));
            Cards.Add(new Card(Card.Suits.Jokers, Card.Values.Ace));
        }

        public void Shuffle()
        {
            for (var n = Cards.Count - 1; n > 0; --n)
            {
                var shuffledCardIndex = random.Next(n + 1);
                var tempCard = Cards[n];
                Cards[n] = Cards[shuffledCardIndex];
                Cards[shuffledCardIndex] = tempCard;
            }
        }

        public List<Card> DealHand(int numberOfCards)
        {
            var hand = new List<Card>();
            var dealtCards = Cards.Take(numberOfCards).ToList();                      

            dealtCards.ForEach(c => Cards.Remove(c));
            hand.AddRange(dealtCards);

            return hand;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public Card GetTopCard()
        {
            return Cards.First();
        }

        #region IEquatable
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object other)
        {
           return EqualDecks(this, other as Deck);
        }

        public static bool operator ==(Deck x, Deck y)
        {
            return EqualDecks(x, y);
        }
        public static bool operator !=(Deck x, Deck y)
        {
            return !EqualDecks(x, y);
        }

        private static bool EqualDecks(Deck x, Deck y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;

            var deck = from card in x.Cards.Select(c => c)
                       select card;
            var otherDeck = from card in y.Cards.Select(c => c)
                            select card;

            return deck.Count() == otherDeck.Count() && deck.Intersect(otherDeck).Count() == otherDeck.Count();
        }
        #endregion
    }
}

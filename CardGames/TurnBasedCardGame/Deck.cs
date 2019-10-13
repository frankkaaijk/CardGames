using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections;

[assembly: InternalsVisibleTo("TurnBasedCardGameTests")]
namespace TurnBasedCardGame
{
    public class Deck : IEquatable<object>
    {
        public enum DeckType
        {
            Empty,            
            Standard,
            StandardWithJokers
        }

        internal static Random random = new Random();
        internal Stack<Card> Cards;

        public Deck()
        {
            Cards = new Stack<Card>();
            Initialize(DeckType.Empty);
        }
        public Deck(DeckType deckType)
        {
            Cards = new Stack<Card>();
            Initialize(deckType);
        }

        private void Initialize(DeckType deckType)
        {
            switch (deckType)
            {
                case DeckType.Empty:
                    break;
                case DeckType.Standard:
                    FillDeck();
                    break;
                case DeckType.StandardWithJokers:
                    FillDeck();
                    // Add 2 jokers
                    Cards.Push(new Card(Card.Suits.Jokers, Card.Values.Ace));
                    Cards.Push(new Card(Card.Suits.Jokers, Card.Values.Ace));
                    break;
                default:
                    throw new IndexOutOfRangeException("Unknown DeckType");
            }
        }
        public void Shuffle()
        {
            var cards = Cards.ToArray();
            Cards.Clear();
            foreach (var card in cards.OrderBy(c => random.Next()))
                Cards.Push(card);
        }
        public List<Card> DealHand(int numberOfCards)
        {
            var hand = new List<Card>();

            for (var i = 0; i < numberOfCards; i++)
            {
                hand.Add(Cards.Pop());
            }
            return hand;
        }
        public void AddCard(Card card)
        {
            Cards.Push(card);
        }
        public Card TopOfDeck()
        {
            return Cards.Peek();
        }
        private void FillDeck()
        {
            foreach (var suit in (Card.Suits[])Enum.GetValues(typeof(Card.Suits)))
            {
                if (suit != Card.Suits.Jokers)
                {
                    foreach (var value in (Card.Values[])Enum.GetValues(typeof(Card.Values)))
                    {
                        Cards.Push(new Card(suit, value));
                    }
                }
            }
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

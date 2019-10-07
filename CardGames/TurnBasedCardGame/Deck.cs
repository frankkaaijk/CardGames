using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DeckTest")]
namespace TurnBasedCardGame
{
    public class Deck
    {
        internal List<Card> Cards;

        public Deck()
        {
            Cards = new List<Card>();
            Initialize();
        }

        private void Initialize()
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
            Cards.Add(new Card(Card.Suits.Jokers, Card.Values.Void));
            Cards.Add(new Card(Card.Suits.Jokers, Card.Values.Void));
        }

        public void Shuffle()
        {

        }
    }
}

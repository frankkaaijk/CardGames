using System;

namespace TurnBasedCardGame
{
    public class Card
    {
        public enum Suits
        {
            Hearts,
            Spades,
            Diamonds,
            Clubs,
            Jokers
        }

        public enum Values
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Void
        }

        public Values Value { get; }
        public Suits Suit { get; }
        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }
    }
}

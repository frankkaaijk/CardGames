using System;

namespace TurnBasedCardGame
{
    public class Card : IEquatable<object>   
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
            King
        }

        public Values Value { get; }
        public Suits Suit { get; }
        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            if( Suit == Suits.Jokers )
            {
                return $"{Enum.GetName(typeof(Card.Suits), Suit)}" +
                System.Environment.NewLine;
            }

            return $"{Enum.GetName(typeof(Card.Values), Value)} of " +
                $"{Enum.GetName(typeof(Card.Suits), Suit)}" +
                System.Environment.NewLine;
        }

        #region IEquatable

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return EqualsCard(this, other as Card);
        }

        public static bool operator ==(Card x, Card y)
        {
            return EqualsCard(x, y);
        }
        public static bool operator !=(Card x, Card y)
        {
            return !EqualsCard(x, y);
        }

        private static bool EqualsCard(Card x, Card y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;

           return x.Suit == y.Suit && x.Value == y.Value;
        }
        #endregion
    }
}

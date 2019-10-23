using System;
using System.Text.RegularExpressions;
using log4net;

namespace CardGames
{
    public class Card : IEquatable<object>   
    {
        private static readonly ILog log = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                return $"{Enum.GetName(typeof(Card.Suits), Suit)}";
            }

            return $"{Enum.GetName(typeof(Card.Values), Value)} of " +
                $"{Enum.GetName(typeof(Card.Suits), Suit)}";
        }

        public static bool TryParse(string stringToParse, out Card card)
        {
            var expr = @"\sof\s";
            var matches = Regex.Split(stringToParse, expr, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            var message = $"{stringToParse} parsed the following matches {matches[0]} ";
            if (matches.Length > 1)
            {
                message += $"and {matches[1]}.";
            }
            log.Debug(message);
            

            Suits cardSuit;
            Values cardValue;

            // Jokers are the odd one out, they 
            if ("jokers" == matches[0].ToLower())
            {
                card = new Card(Suits.Jokers, Values.Ace);
                return true;
            }

            if( Enum.TryParse(matches[0], true, out cardValue) && 
                Enum.TryParse(matches[1], true, out cardSuit))
            {
                card = new Card(cardSuit, cardValue);
                return true;
            }

            card = null;
            return false;
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

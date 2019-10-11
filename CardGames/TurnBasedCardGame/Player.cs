using System;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedCardGame
{
    internal class Player : IEquatable<object>
    {
        public List<Card> Hand { get; set; }
        public string Name { get; } = string.Empty;

        public Player(string name)
        {
            Name = name;
        }

        public void GiveHand(List<Card> cards)
        {
            Hand = cards;
        }
        public string ShowHand()
        {
            var playerHand = string.Empty;
            foreach(Card card in Hand)
            {
                playerHand += card.ToString();
            }

            return playerHand;
        }

        #region IEquatable
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return EqualPlayers(this, other as Player);
        }

        public static bool operator ==(Player x, Player y)
        {
            return EqualPlayers(x, y);
        }
        public static bool operator !=(Player x, Player y)
        {
            return !EqualPlayers(x, y);
        }

        private static bool EqualPlayers(Player x, Player y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;

            return x.Name == y.Name;
        }
        #endregion
    }
}

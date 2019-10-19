using System;
using System.Collections.Generic;

namespace TurnBasedCardGame
{
    public class Player : IEquatable<object>
    {
        internal List<Card> Hand { get; set; }
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
            List<string> cardsInHand = new List<string>();

            foreach (var card in Hand)
            {
                cardsInHand.Add(card.ToString());                
            }
                        
            return string.Join(Environment.NewLine, cardsInHand);
        }
        public void RemoveCardFromHand(Card card)
        {
            if(!Hand.Remove(card))
            {
                throw new InvalidOperationException("No such card in the players hand");
            }

        }
        public bool HasCard(Card card)
        {
            return Hand.Contains(card);
        }
        public void AddCardToHand(Card card)
        {
            Hand.Add(card);
        }

        public void AddCardsToHand(List<Card> cards)
        {
            foreach(var card in cards)
            {
                AddCardToHand(card);
            }
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

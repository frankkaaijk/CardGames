using System;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedCardGame
{
    public class Player
    {
        public List<Card> Hand { get; set; }
        private Card CardToPlay { get; set; }
        private readonly String Name;

        public Player(string name)
        {
            Name = name;
        }

        public void GiveHand(List<Card> cards)
        {
            Hand = cards;
        }
        public void Hit(Card card)
        {
            if (Hand.Contains(card))
            {
                CardToPlay = card;
            }
        }
        public void Stay()
        { }

        public void Quit()
        {

        }
    }
}

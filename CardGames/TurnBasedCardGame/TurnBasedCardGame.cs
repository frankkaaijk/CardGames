using System;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedCardGame
{
    public class TurnBasedCardGame
    {
        internal List<Player> Players;
        internal Deck Deck;

        public TurnBasedCardGame()
        {
            Deck = new Deck();
            Deck.Shuffle();
        }

        public void AddPlayer(Player player)
        {
            player.GiveHand(Deck.DealHand(4));
            Players.Add(player);
        }

    }
}

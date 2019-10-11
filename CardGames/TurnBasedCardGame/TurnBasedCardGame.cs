using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBasedCardGame
{
    public class CardGame
    {
        internal HashSet<Player> Players;
        internal Deck Deck;
        internal List<Card> PlayedCards;
        //private ICardGame CardGame;

        public CardGame()
        {
            Deck = new Deck();                  // Deck to play with
            Deck.Shuffle();                     // Shuffle the deck
            Players = new HashSet<Player>();    // Initialize list of players
            PlayedCards = Deck.DealHand(1);     // First card to play with
        }

        public bool AddPlayer(string playerName)
        {
            var newPlayer = new Player(playerName);
            newPlayer.GiveHand(Deck.DealHand(4));
            return Players.Add(newPlayer);
        }

        public string ShowLastPlayedCard()
        {
            return PlayedCards.Last().ToString();
        }
    }
}

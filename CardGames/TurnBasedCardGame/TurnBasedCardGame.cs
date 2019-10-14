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
        internal Deck PlayedCardsDeck;
        internal Player CurrentPlayer;
        internal bool GameInProgress = false;
        //private ICardGame CardGame;

        public CardGame()
        {
            Deck = new Deck(Deck.DeckType.StandardWithJokers);  // Deck to play with
            PlayedCardsDeck = new Deck();                           // Deck to stack on
            Deck.Shuffle();                                     // Shuffle the deck
            Players = new HashSet<Player>();                    // Initialize list of players
            PlayedCardsDeck.AddCard(Deck.DealHand(1).First());      // First card to play with
        }

        public bool AddPlayer(string playerName)
        {

            if(GameInProgress)
            {
                throw new InvalidOperationException("Game already in progress");
            }
            var newPlayer = new Player(playerName);
            newPlayer.GiveHand(Deck.DealHand(4));
            return Players.Add(newPlayer);
            
        }

        public string ShowLastPlayedCard()
        {
            return PlayedCardsDeck.TopOfDeck().ToString();
        }

        public string ShowPlayersHand(string playername)
        {
            var player = Players.FirstOrDefault(x => x.Name == playername);
            return player.ShowHand();
        }

        public void StartGame()
        {
            GameInProgress = true;
        }

        public bool Hit(string playername, string cardString)
        {
            if (!GameInProgress)
            {
                throw new InvalidOperationException("Game not in progress");
            }

            Card cardToPlay;
            if ( !Card.TryParse(cardString, out cardToPlay) )
            {
                throw new FormatException("Invalid (unknown) card");
            }

            // Rules:
            // The top now contains a value two card:
            // The current player must take two cards from the deck pile
            // If empty the played card pile is the new (reshuffled) deck.

            // The top now contains a value eight card:
            // The current player cannot hit

            // The top now contains a Jack
            // The current player restates suit

            // The top now contains a King
            // The current play direction is reversed

            // The top now contains Joker suit
            // The current player takes 5 cards from played card pile.

            // 1. Card is of same value or same suit
            if( PlayedCardsDeck.GetTopCard().Suit == cardToPlay.Suit ||
                PlayedCardsDeck.GetTopCard().Value == cardToPlay.Value )
            {
                PlayedCardsDeck.AddCard(cardToPlay);
                // Correct move, remove card from player hand
                var player = Players.FirstOrDefault(x => x.Name == playername);
                player.RemoveCardFromHand(cardToPlay);

                EvaluateGame();

                return true;
            }

            // Incorrect card, invalid hit so stop
            return false;

        }
        public void Stay(string playername)
        {
            var player = Players.FirstOrDefault(x => x.Name == playername);
            if( PlayedCardsDeck.GetTopCard()==null)
            {
                // There is only one card left 
                // Return all played cards to the deck and reshuffle

            }

            player.AddCardToHand(Deck.DealHand(1).First());
        }

        private bool EvaluateGame()
        {
            foreach(var player in Players)
            {
                if(player.Hand.Count == 0)
                {
                    // Game is won!
                    GameInProgress = false;
                    return true;
                }
            }
            return false;
        }
    }

}

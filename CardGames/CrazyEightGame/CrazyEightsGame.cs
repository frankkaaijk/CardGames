using CardGame;
using CardGames;
using CrazyEightsGame;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SheddingCardGames
{
    public class CrazyEightsGame : ICardGame
    {
        private static readonly ILog log =
               LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal enum SpecialValues
        {
            Ace = Card.Values.Ace,      // Reverse order of play
            Two = Card.Values.Two,      // Next player get two cards
            Five = Card.Values.Five,    // (reserved)
            Seven = Card.Values.Seven,  // Current player can play another card
            Eight = Card.Values.Eight,  // Next player must skip
            Ten = Card.Values.Ten,      // (reserved)
            Jack = Card.Values.Jack,    // (reserved)
            King = Card.Values.King     // (reserved)
        }

        internal Deck PlayDeck = new Deck(Deck.DeckType.FrenchIncludingJokers);
        internal Deck DiscardPile = new Deck(Deck.DeckType.Empty);
        internal CrazyEightsPlayers Players = new CrazyEightsPlayers();
        
        public CrazyEightsGame(CrazyEightsPlayers crazyEightsPlayers)
        {
            PlayDeck.Shuffle();
            DiscardPile.AddCard(PlayDeck.DealHand(1).First()); // First card to play with 
            Players = crazyEightsPlayers;
        }

        public bool AddPlayer(Player player)
        {
            try
            {
                Players.AddPlayer(player);
            }
            catch (NotImplementedException)
            {
                return false;
            }
            return true;
        }
            
        public void DealHands()
        {
            if( Players.Count < 2)
            {
                throw new NotImplementedException("Minimum of two players are required");
            }

            var numberOfCards = Players.Count == 2 ? 7 : 5;
            
            foreach(Player player in Players)
            {
                player.GiveHand(PlayDeck.DealHand(numberOfCards));
            }            
        }

        public void TakeTurn(Player player, Card card)
        {
            if(Players.Contains(player) == false)
            {
                throw new InvalidOperationException("Unknown player");
            }

            if (IsValidCard(card))
            {
                // Update discard pile
                DiscardPile.AddCard(card);
                // Update (current) player hand.
                Players.GetPlayer().RemoveFromHand(card);

                EvaluateSpecialCards(card);
            }
        }
        
        public void SkipTurn(Player player)
        {
            Players.NextPlayer();
            Players.GetPlayer().AddToHand(PlayDeck.DealHand(1));
        }

        private bool IsValidCard(Card cardToPlay)
        {
            // Card is a Joker or of same value or same suit 
            return (cardToPlay.Suit == Card.Suits.Jokers ||
                (DiscardPile.GetTopCard().Suit == cardToPlay.Suit ||
                 DiscardPile.GetTopCard().Value == cardToPlay.Value ||
                 DiscardPile.GetTopCard().Suit == Card.Suits.Jokers));                 
        }

        private void EvaluateSpecialCards(Card cardToPlay)
        {
            if (Card.Suits.Jokers == cardToPlay.Suit)
            {
                Players.NextPlayer();
                Players.GetPlayer().AddToHand(PlayDeck.DealHand(5));
            }

            // Either the suit or value was correct so let's see if the card was a special one
            // Value of 2?
            switch (cardToPlay.Value)
            {
                case (Card.Values)SpecialValues.Ace:
                    {
                        Players.PlayDirection = Players.PlayDirection == PlayDirection.Clockwise ? PlayDirection.CounterClockWise : PlayDirection.Clockwise;
                        Players.NextPlayer();
                        break;
                    }
                case (Card.Values)SpecialValues.Two:
                    {
                        Players.NextPlayer();
                        Players.GetPlayer().AddToHand(PlayDeck.DealHand(2));
                        break;                        
                    }
                case (Card.Values)SpecialValues.Seven:
                    {
                        // Nothing
                        break;
                    }
                case (Card.Values)SpecialValues.Eight:
                    {
                        Players.NextPlayer();
                        Players.NextPlayer();
                        break;
                    }
                default:
                    Players.NextPlayer();
                    break;
            }
        }

        private void ResetDecks()
        {
            ResetPlayDeck();
            ResetDiscardDeck();
        }

        private void ResetPlayDeck()
        {
            // not enough cards left in the play deck
            // Copy the discard deck to the playdeck and shuffle
            PlayDeck.FillDeck(DiscardPile);
            PlayDeck.Shuffle();
        }
        private void ResetDiscardDeck()
        {
            // save the top card from the discard deck
            var topCard = DiscardPile.GetTopCard();
            // Clear discarddeck
            DiscardPile.Clear();
            DiscardPile.AddCard(topCard);
        }

        public Card TopOfDeck()
        {
            return DiscardPile.GetTopCard();
        }
    }
}
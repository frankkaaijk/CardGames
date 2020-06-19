using CardGames;
using log4net;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CrazyEightsGameTests")]
namespace SheddingCardGames.CrazyEightsGame.States
{
    public class GameInProgressState : GameState
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

        public GameInProgressState(GameState gameState) : this(gameState.CrazyEightsGame)
        {
        }

        public GameInProgressState(CrazyEightsGame game)
        {
            CrazyEightsGame = game;
            CrazyEightsGame.DiscardPile.AddCard(CrazyEightsGame.PlayDeck.DealHand(1).First()); // First card to play with 
        }

        public override bool AddPlayer(Player player)
        {
            throw new InvalidOperationException("Cannot add players to game in progress.");
        }
        public override Player GetPlayer()
        {
            return CrazyEightsGame.Players.GetPlayer();
        }

        public override void DealHands()
        {
            throw new InvalidOperationException("Cannot deal hands when game is in progress.");
        }

        public override void TakeTurn(Player player, Card card)
        {
            if(CrazyEightsGame.Players.Contains(player) == false)
            {
                throw new InvalidOperationException("Unknown player");
            }

            if(CrazyEightsGame.Players.GetPlayer() != player)
            {
                throw new InvalidOperationException("Player not at play");
            }

            if (CrazyEightsGame.Players.GetPlayer().ShowHand().Contains(card) && IsValidCard(card))
            {
                // Update discard pile
                CrazyEightsGame.DiscardPile.AddCard(card);
                // Update (current) player hand.
                CrazyEightsGame.Players.GetPlayer().RemoveFromHand(card);

                EvaluateSpecialCards(card);
            }

            EvaluateGame();
        }
        
        public override void SkipTurn(Player player)
        {
            CrazyEightsGame.Players.GetPlayer().AddToHand(CrazyEightsGame.PlayDeck.DealHand(1));
            CrazyEightsGame.Players.NextPlayer();            
        }
        
        public override Card TopOfDeck()
        {
            return CrazyEightsGame.DiscardPile.GetTopCard();
        }

        private bool IsValidCard(Card cardToPlay)
        {
            // Card is a Joker or of same value or same suit 
            return (cardToPlay.Suit == Card.Suits.Jokers ||
                (CrazyEightsGame.DiscardPile.GetTopCard().Suit == cardToPlay.Suit ||
                 CrazyEightsGame.DiscardPile.GetTopCard().Value == cardToPlay.Value ||
                 CrazyEightsGame.DiscardPile.GetTopCard().Suit == Card.Suits.Jokers));                 
        }

        private void EvaluateSpecialCards(Card cardToPlay)
        {
            if (Card.Suits.Jokers == cardToPlay.Suit)
            {
                CrazyEightsGame.Players.NextPlayer();
                try
                {
                    CrazyEightsGame.Players.GetPlayer().AddToHand(CrazyEightsGame.PlayDeck.DealHand(5));
                }
                catch (InvalidOperationException)
                {
                    ResetDecks();
                    CrazyEightsGame.Players.GetPlayer().AddToHand(CrazyEightsGame.PlayDeck.DealHand(5));
                }
                return;                
            }

            // Either the suit or value was correct so let's see if the card was a special one
            // Value of 2?
            switch (cardToPlay.Value)
            {
                case (Card.Values)SpecialValues.Ace:
                    {
                        CrazyEightsGame.Players.PlayDirection = CrazyEightsGame.Players.PlayDirection == PlayDirection.Clockwise ? PlayDirection.CounterClockWise : PlayDirection.Clockwise;
                        CrazyEightsGame.Players.NextPlayer();
                        break;
                    }
                case (Card.Values)SpecialValues.Two:
                    {
                        CrazyEightsGame.Players.NextPlayer();
                        try
                        {
                            CrazyEightsGame.Players.GetPlayer().AddToHand(CrazyEightsGame.PlayDeck.DealHand(2));
                        }
                        catch (InvalidOperationException)
                        {
                            ResetDecks();
                            CrazyEightsGame.Players.GetPlayer().AddToHand(CrazyEightsGame.PlayDeck.DealHand(2));
                        }
                        break;                        
                    }
                case (Card.Values)SpecialValues.Seven:
                    {
                        // Nothing
                        break;
                    }
                case (Card.Values)SpecialValues.Eight:
                    {
                        CrazyEightsGame.Players.NextPlayer();
                        CrazyEightsGame.Players.NextPlayer();
                        break;
                    }
                default:
                    CrazyEightsGame.Players.NextPlayer();
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
            CrazyEightsGame.PlayDeck.FillDeck(CrazyEightsGame.DiscardPile);
            CrazyEightsGame.PlayDeck.Shuffle();
        }
        private void ResetDiscardDeck()
        {
            // save the top card from the discard deck
            var topCard = CrazyEightsGame.DiscardPile.GetTopCard();
            // Clear discarddeck
            CrazyEightsGame.DiscardPile.Clear();
            CrazyEightsGame.DiscardPile.AddCard(topCard);
        }
        private void EvaluateGame()
        {
            foreach(Player player in CrazyEightsGame.Players)
            {
                if(player.ShowHand().Count == 0)
                {
                    CrazyEightsGame.State = new GameWonState(this);
                }
            }
        }
    }
}
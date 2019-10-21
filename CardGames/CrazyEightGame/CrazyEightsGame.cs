using System;
using System.Linq;
using CardGames;
using System.Collections.Generic;

namespace SheddingCardGames
{
    public class CrazyEightsGame : ICardGame
    {
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
        internal Deck DiscardDeck = new Deck(Deck.DeckType.Empty);

        public CrazyEightsGame()
        {
            PlayDeck.Shuffle();
            DiscardDeck.AddCard(PlayDeck.DealHand(1).First()); // First card to play with         
        }

        public Card ShowTopOfDeck()
        {
            return DiscardDeck.TopOfDeck();
        }

        public void DealHand(ref Player player)
        {
            player.GiveHand(PlayDeck.DealHand(4));
        }

        public NextMove Hit(Card card, ref Player player, ref Player nextPlayer)
        {
            // Don't move the game forward on an invalid hit (invalid/unknown card)
            var nextMove = NextMove.CurrentPlayer;

            if (!player.HandHasCard(card))
            {
                return nextMove;
            }

            if (SameSuitOrValueRule(player, card))
            {
                nextMove = EvaluateSpecialCards(player, nextPlayer, card);
            }
            if (EvaluateGame(player))
            {
                nextMove = NextMove.GameWon;
            }
            return nextMove;
        }

        public NextMove Stay(ref Player player)
        {
            try
            {
                player.AddToHand(PlayDeck.DealHand(1).First());
            }
            catch (InvalidOperationException)
            {
                // not enough cards in the play deck
                // save the top card from the discard deck
                var topCard = DiscardDeck.GetTopCard();

                // Copy the discard deck to the playdeck and shuffle
                PlayDeck.FillDeck(DiscardDeck);
                PlayDeck.Shuffle();

                // Clear discarddeck
                DiscardDeck.Clear();
                DiscardDeck.AddCard(topCard);
            }
            // EvaluateGame(player); (not necessary, card was just dealt)
            return NextMove.NextPlayer;
        }

        private bool EvaluateGame(Player player)
        {
            if (player.ShowHand() == string.Empty)
            {
                return true;
            }
            return false;
        }

        private bool SameSuitOrValueRule(Player player, Card cardToPlay)
        {
            // Card is a Joker or of same value or same suit 
            if (cardToPlay.Suit == Card.Suits.Jokers ||
                (DiscardDeck.GetTopCard().Suit == cardToPlay.Suit ||
                 DiscardDeck.GetTopCard().Value == cardToPlay.Value || 
                 DiscardDeck.GetTopCard().Suit == Card.Suits.Jokers))
            {
                DiscardDeck.AddCard(cardToPlay);
                player.RemoveFromHand(cardToPlay);
                return true;
            }
            return false;
        }

        private NextMove EvaluateSpecialCards(Player player, Player nextPlayer, Card cardToPlay)
        {
            if (Card.Suits.Jokers == cardToPlay.Suit)
            {
                nextPlayer.AddToHand(PlayDeck.DealHand(5));
                return NextMove.NextPlayer;
            }

            // Either the suit or value was correct so let's see if the card was a special one
            // Value of 2?
            switch (cardToPlay.Value)
            {
                case (Card.Values)SpecialValues.Ace:
                    {
                        return NextMove.ReservePlayOrder;
                    }
                case (Card.Values)SpecialValues.Two:
                    {
                        nextPlayer.AddToHand(PlayDeck.DealHand(2));
                        return NextMove.NextPlayer;
                    }
                case (Card.Values)SpecialValues.Seven:
                    {
                        return NextMove.CurrentPlayer;
                    }
                case (Card.Values)SpecialValues.Eight:
                    {
                        return NextMove.SkipNextPlayer;
                    }
                default:
                    return NextMove.NextPlayer;
            }
        }
    }
}
using System;
using System.Linq;
using TurnBasedCardGame;
using System.Collections.Generic;

namespace SheddingCardGames
{
    public class CrazyEightGame : ICardGame
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

        public CrazyEightGame()
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

        public NextMove Hit(Card card, Player player, Player nextPlayer)
        {
            var nextMove = NextMove.NextPlayer;
            if (SameSuitOrValueRule(player, card))
            {
                nextMove = EvaluateSpecialCards(player, nextPlayer, card);
            }
            EvaluateGame();
            return nextMove;
        }

        public NextMove Stay(Player player)
        {
            player.AddCardToHand(PlayDeck.DealHand(1).First());
            EvaluateGame();
            return NextMove.NextPlayer;
        }

        private bool EvaluateGame()
        {
            /*&foreach (var player in Players)
            {
                if (player.Hand.Count == 0)
                {
                    // Game is won!
                    GameInProgress = false;
                    return true;
                }
            }*/
            return false;
        }

        private bool SameSuitOrValueRule(Player player, Card cardToPlay)
        {
            // Card is a Joker or of same value or same suit 
            if (cardToPlay.Suit == Card.Suits.Jokers ||
                (DiscardDeck.GetTopCard().Suit == cardToPlay.Suit ||
                 DiscardDeck.GetTopCard().Value == cardToPlay.Value))
            {
                DiscardDeck.AddCard(cardToPlay);
                player.RemoveCardFromHand(cardToPlay);
                return true;
            }
            return false;
        }

        private NextMove EvaluateSpecialCards(Player player, Player nextPlayer, Card cardToPlay)
        {
            // Either the suit or value was correct so let's see if the card was a special one
            // Value of 2?
            switch (cardToPlay.Value)
            {
                case (Card.Values)SpecialValues.Two:
                    {
                        nextPlayer.AddCardsToHand(PlayDeck.DealHand(2));
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

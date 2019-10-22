using System;
using System.Collections.Generic;
using System.Linq;
using CardGames;

namespace CrazyEightsGame.States
{
    class CrazyEightsWon : ICardGameCommands
    {
        internal SheddingCardGames.CrazyEightsGame Game;

        public CrazyEightsWon(SheddingCardGames.CrazyEightsGame game)
        {
            Game = game;            
        }
        public List<Card> DealHand()
        {
            throw new NotSupportedException("You cannot deal a hand when the game is won");
        }

        public NextMove Hit(Card card, ref Player player, ref Player nextPlayer)
        {
            throw new NotSupportedException("You cannot hit when the game is won");
        }

        public Card ShowTopOfDeck()
        {
            throw new NotSupportedException("You cannot show the top of the deck when the game is won");
        }

        public NextMove Stay(ref Player player)
        {
            throw new NotSupportedException("You cannot stay when the game is won");
        }

        public void SetState(GameStates state)
        {
            switch (state)
            {
                case (GameStates.Playing):
                    {
                        throw new NotSupportedException("Cannot set state to playing from won");
                    }
                case (GameStates.Won):
                    {
                        // Log that we're already won, but keep going
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException("Unknown state");
                    }
            }
        }
    }
}

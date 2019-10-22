using System;
using CardGames;
using System.Collections.Generic;
using CrazyEightsGame.States;
using System.Diagnostics;

namespace SheddingCardGames
{
    public class CrazyEightsGame : ICardGameCommands
    {
        private ICardGameCommands StateCommands;
        private GameStates GameState;
        
        public GameStates CrazyEightsGameState
        {
            get { return GameState; }
            set
            {
                GameState = value;
                switch (GameState)
                {
                    case (GameStates.Playing):
                        {
                            StateCommands = new CrazyEightsPlaying(this);
                            break;
                        }
                    case (GameStates.Won):
                        {
                            StateCommands = new CrazyEightsWon(this);
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException("Unknown state");
                        }
                }
            }
        }

        public CrazyEightsGame()
        {
            CrazyEightsGameState = GameStates.Playing;
        }

         public Card ShowTopOfDeck()
        {
            return StateCommands.ShowTopOfDeck();
        }

        public List<Card> DealHand()
        {
            return StateCommands.DealHand();
        }

        public NextMove Hit(Card card, ref Player player, ref Player nextPlayer)
        {
            return StateCommands.Hit(card, ref player, ref nextPlayer);
        }

        public NextMove Stay(ref Player player)
        {
            return StateCommands.Stay(ref player);
        }

        public void SetState(GameStates state)
        {
            StateCommands.SetState(state);
        }
    }
}
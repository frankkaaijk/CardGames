using CardGames;
using System;

namespace SheddingCardGames.CrazyEightsGame.States
{
    public class GameWonState : GameState
    {
        public GameWonState(GameState gameState)
        {
            CrazyEightsGame = gameState.CrazyEightsGame;
        }
        public GameWonState(CrazyEightsGame game)
        {
            CrazyEightsGame = game;
        }

        public override bool AddPlayer(Player player)
        {
            throw new InvalidOperationException();
        }
                public override Player GetPlayer()
        {
            throw new NotImplementedException();
        }

        public override void DealHands()
        {
            throw new InvalidOperationException();
        }

        public override void SkipTurn(Player player)
        {
            throw new InvalidOperationException();
        }

        public override void TakeTurn(Player player, Card card)
        {
            throw new InvalidOperationException();
        }

        public override Card TopOfDeck()
        {
            throw new InvalidOperationException();
        }
    }
}

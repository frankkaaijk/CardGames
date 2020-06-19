using CardGames;
using log4net;
using System;

namespace SheddingCardGames.CrazyEightsGame.States
{
    public class GameSetupState : GameState
    {
        private static readonly ILog log =
               LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GameSetupState(GameState gameState) 
        {
            CrazyEightsGame = gameState.CrazyEightsGame;
        }

        public GameSetupState(CrazyEightsGame game)
        {
            CrazyEightsGame = game;
        }

        public override bool AddPlayer(Player player)
        {
            try
            {
                CrazyEightsGame.Players.AddPlayer(player);
            }
            catch (NotImplementedException)
            {
                return false;
            }
            return true;
        }

        public override Player GetPlayer()
        {
            throw new NotImplementedException();
        }

        public override void DealHands()
        {
            CrazyEightsGame.PlayDeck.Shuffle();

            if (CrazyEightsGame.Players.Count < 2)
            {
                throw new NotImplementedException("Minimum of two players are required");
            }

            var numberOfCards = CrazyEightsGame.Players.Count == 2 ? 7 : 5;

            try
            {
                foreach (Player player in CrazyEightsGame.Players)
                {
                    player.GiveHand(CrazyEightsGame.PlayDeck.DealHand(numberOfCards));
                }
            }
            catch (InvalidOperationException e)
            {
                // Shouldn't happen, game is just initialized.
                log.Error("Not enough cards in playdeck");
                throw e;
            }

            CrazyEightsGame.State = new GameInProgressState(this);
        }

        public override void SkipTurn(Player player)
        {
            throw new NotImplementedException();
        }

        public override void TakeTurn(Player player, Card card)
        {
            throw new NotImplementedException();
        }

        public override Card TopOfDeck()
        {
            throw new NotImplementedException();
        }
    }
}

using CardGames;
using SheddingCardGames;

namespace SheddingCardGames.CrazyEightsGame.States
{
    public abstract class GameState : ICardGame
    {
        public CrazyEightsGame CrazyEightsGame { get; set; }
        
        public abstract bool AddPlayer(Player player);
        public abstract Player GetPlayer();
        public abstract void DealHands();
        public abstract void SkipTurn(Player player);
        public abstract void TakeTurn(Player player, Card card);
        public abstract Card TopOfDeck();
    }
}

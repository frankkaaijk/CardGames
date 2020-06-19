using CardGames;
using SheddingCardGames.CrazyEightsGame.States;

namespace SheddingCardGames.CrazyEightsGame
{
    public class CrazyEightsGame
    {
        public GameState State { get; set; }
        public Deck PlayDeck { get; set; }
        public Deck DiscardPile { get; set; }
        public CrazyEightsPlayers Players { get; set; }

        public CrazyEightsGame() 
        {
            State = new GameSetupState(this);
            PlayDeck = new Deck(DeckType.FrenchIncludingJokers);
            DiscardPile = new Deck(DeckType.Empty);
            Players = new CrazyEightsPlayers();
        }

        public bool AddPlayer(Player player)
        {
            return State.AddPlayer(player);
        }

        public Player GetPlayer()
        {
            return State.GetPlayer();
        }

        public void DealHands()
        {
            State.DealHands();
        }

        public void SkipTurn(Player player)
        {
            State.SkipTurn(player);
        }

        public void TakeTurn(Player player, Card card)
        {
            State.TakeTurn(player, card);
        }
        public Card TopOfDeck()
        {
            return State.TopOfDeck();            
        }
    }
}

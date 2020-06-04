using CardGames;

namespace CardGame
{
    public interface ICardGame
    {
        bool AddPlayer(Player player);
        void DealHands();
        void TakeTurn(Player player, Card card);
        void SkipTurn(Player player);
        Card TopOfDeck();
    }
}

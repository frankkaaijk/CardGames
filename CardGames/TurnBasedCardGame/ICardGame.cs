namespace CardGames
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

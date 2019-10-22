using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames
{
    public enum GameStates
    {
        Playing,
        Won
    }
    public interface ICardGameCommands
    {
        
        NextMove Hit(Card card, ref Player player, ref Player nextPlayer);
        NextMove Stay(ref Player player);
        List<Card> DealHand();
        Card ShowTopOfDeck();
        void SetState(GameStates state);
    }
}

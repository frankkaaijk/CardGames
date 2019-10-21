using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames
{
    public interface ICardGame
    {
        NextMove Hit(Card card, ref Player player, ref Player nextPlayer);
        NextMove Stay(ref Player player);
        void DealHand(ref Player player);
        Card ShowTopOfDeck();
    }
}

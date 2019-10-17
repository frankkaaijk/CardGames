using System;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedCardGame
{
    public interface ICardGame
    {
        NextMove Hit(Card card, Player player, Player nextPlayer);
        NextMove Stay(Player player);
        void DealHand(ref Player player);
        Card ShowTopOfDeck();
    }
}

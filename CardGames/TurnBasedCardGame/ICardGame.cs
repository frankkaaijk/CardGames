using System;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedCardGame
{
    public interface ICardGame
    {
        void Hit(Card card);
        void Stay();
    }
}

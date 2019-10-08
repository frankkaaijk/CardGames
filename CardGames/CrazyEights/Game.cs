using System;
using TurnBasedCardGame;
using System.Collections.Generic;

namespace CrazyEights
{
    public class Game : ICardGame
    {
        private List<Card> played;

        public void Hit(Card card)
        {
            throw new NotImplementedException();
        }

        public void Stay()
        {
            throw new NotImplementedException();
        }
    }
}

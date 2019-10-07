using System;
using Xunit;
using TurnBasedCardGame;

namespace TurnBasedCardGameTests
{
    public class DeckTest
    {
        [Fact]
        public void CreateDeck()
        {
            var deck = new Deck();

            Assert.Equal(54, deck.Cards.Count());
        }
    }
}

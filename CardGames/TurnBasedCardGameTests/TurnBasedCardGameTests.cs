using System;
using System.Collections.Generic;
using System.Text;
using TurnBasedCardGame;
using Xunit;

namespace TurnBasedCardGameTests
{
    public class TurnBasedCardGameTests
    {
        [Fact]
        public void AddPlayersTest()
        {
            var game = new TurnBasedCardGame.CardGame();
            Assert.True(game.AddPlayer("Test"));
            Assert.False(game.AddPlayer("Test"));
            Assert.True(game.AddPlayer("Test2"));            
        }

        [Fact]
        public void StartGame()
        {
            var game = new TurnBasedCardGame.CardGame();
            Assert.NotNull(game.ShowLastPlayedCard());
        }
    }
}

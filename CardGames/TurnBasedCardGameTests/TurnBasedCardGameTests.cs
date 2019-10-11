using System;
using System.Collections.Generic;
using System.Text;
using TurnBasedCardGame;
using Xunit;

namespace TurnBasedCardGameTests
{
    public class TurnBasedCardGameTests
    {
        private CardGame Game;
        public TurnBasedCardGameTests()
        {
            Game = new CardGame();
        }

        [Fact]
        public void AddPlayers()
        {
            Assert.True(Game.AddPlayer("Test"));
            Assert.False(Game.AddPlayer("Test"));
            Assert.True(Game.AddPlayer("Test2"));
            Game.StartGame();
            Assert.Throws<InvalidOperationException>(() => Game.AddPlayer("Test3"));
        }

        [Fact]
        public void StartGame()
        {
            Assert.NotNull(Game.ShowLastPlayedCard());           
        }

        [Fact]
        public void Hit()
        {
            Game.AddPlayer("Test1");
            Assert.Throws<InvalidOperationException>(() => Game.Hit("Test1", "King of Hearts"));
            Game.StartGame();
            Assert.Throws<FormatException>(() => Game.Hit("Test1", "Hearts of King"));
            Game.Hit("Test1", "King of Hearts");
        }
    }
}

using System;
using TurnBasedCardGame;
using Xunit;

namespace TurnBasedCardGameTests
{
    public class TurnBasedCardGameTests
    {
        private CardGame Game;
        public TurnBasedCardGameTests()
        {
            var crazyEightGame = new SheddingCardGames.CrazyEightGame();
            Game = new CardGame(crazyEightGame);
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
            Assert.Throws<InvalidOperationException>(() => Game.Hit("King of Hearts"));
            Game.StartGame();
            Assert.Throws<FormatException>(() => Game.Hit("Hearts of King"));
            Game.Hit("King of Hearts");
            Assert.NotNull(Game.ShowLastPlayedCard());
        }

        [Fact]
        public void ProgressGame()
        {
            Game.AddPlayer("Test1");
            Game.AddPlayer("Test2");
            Game.AddPlayer("Test3");
            Game.StartGame();
            Assert.Equal("Test1", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.NextPlayer);
            Assert.Equal("Test2", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.NextPlayer);
            Assert.Equal("Test3", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.ReservePlayOrder);
            Game.ProgressPlay(NextMove.NextPlayer);
            Assert.Equal("Test2", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.CurrentPlayer);
            Assert.Equal("Test2", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.ReservePlayOrder);
            Game.ProgressPlay(NextMove.NextPlayer);
            Assert.Equal("Test3", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.NextPlayer);
            Assert.Equal("Test1", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.SkipNextPlayer);
            Assert.Equal("Test3", Game.ShowCurrentPlayer());
        }
    }
}

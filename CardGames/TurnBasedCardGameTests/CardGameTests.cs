using System;
using CardGames;
using Xunit;

namespace CardGameTests
{
    public class CardGameTests
    {
        private CardGames.TurnbasedCardGame Game;
        public CardGameTests()
        {
            var crazyEightGame = new SheddingCardGames.CrazyEightsGame();
            Game = new CardGames.TurnbasedCardGame(crazyEightGame);
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
            Assert.Equal("Test2", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.CurrentPlayer);
            Assert.Equal("Test2", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.ReservePlayOrder);
            Assert.Equal("Test3", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.NextPlayer);
            Assert.Equal("Test1", Game.ShowCurrentPlayer());
            Game.ProgressPlay(NextMove.SkipNextPlayer);
            Assert.Equal("Test3", Game.ShowCurrentPlayer());
        }
    }
}

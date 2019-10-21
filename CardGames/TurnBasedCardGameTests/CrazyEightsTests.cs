using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SheddingCardGames;
using CardGames;

namespace CardGameTests
{
    public class CrazyEightsTests
    {
        [Fact]
        public void HitTest()
        {
            var crazyEightsGame = new CrazyEightsGame();
            var player1 = new Player("Test1");
            var topCard = crazyEightsGame.ShowTopOfDeck();
            player1.GiveHand(new List<Card> {
                new Card(topCard.Suit, Card.Values.Eight),
                new Card(topCard.Suit, Card.Values.Two),
                new Card(Card.Suits.Hearts, Card.Values.Four) });
            var player2 = new Player("Test2");
            player2.GiveHand(new List<Card> { 
                new Card(Card.Suits.Clubs, Card.Values.Six), 
                new Card(Card.Suits.Spades, Card.Values.Three) });

            var result = crazyEightsGame.Hit(new Card(topCard.Suit, Card.Values.Eight), ref player1, ref player2);
            Assert.True(NextMove.SkipNextPlayer == result);
            result = crazyEightsGame.Hit(new Card(topCard.Suit, Card.Values.Two), ref player1, ref player2);
            Assert.True(NextMove.NextPlayer == result);
        }

        [Fact]
        public void StayTest()
        {
            var crazyEightsGame = new CrazyEightsGame();
            var player1 = new Player("Test1");
            player1.GiveHand(new List<Card> { new Card(Card.Suits.Clubs, Card.Values.Eight), new Card(Card.Suits.Hearts, Card.Values.Two) });

            var result = crazyEightsGame.Stay(ref player1);
            Assert.True(NextMove.NextPlayer == result);
            var count = player1.ShowHand().Split(Environment.NewLine).Length;
            Assert.True(3 == count);
        }
    }
}

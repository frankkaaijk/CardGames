using CardGames;
using SheddingCardGames;
using SheddingCardGames.CrazyEightsGame;
using SheddingCardGames.CrazyEightsGame.States;
using System;
using System.Collections.Generic;
using Xunit;

namespace CrazyEightsGameTests
{
    public class CrazyEightsTests
    { 
        public CrazyEightsTests()
        {        
        }

        [Fact]
        public void TakeTurn()
        {
            var CrazyEightsGameUnderTest = new CrazyEightsGame();
            CrazyEightsGameUnderTest.AddPlayer(new Player("Test1"));
            CrazyEightsGameUnderTest.AddPlayer(new Player("Test2"));

            var topCard = new Card(Card.Suits.Diamonds, Card.Values.Eight);
            CrazyEightsGameUnderTest.DiscardPile.AddCard(topCard);

            CrazyEightsGameUnderTest.Players.Players[0].GiveHand(new List<Card> {
                new Card(topCard.Suit, Card.Values.Eight),
                new Card(topCard.Suit, Card.Values.Two),
                new Card(Card.Suits.Hearts, Card.Values.Four) });
            CrazyEightsGameUnderTest.Players.Players[1].GiveHand(new List<Card> {
                new Card(Card.Suits.Clubs, Card.Values.Six),
                new Card(Card.Suits.Spades, Card.Values.Three) });

            CrazyEightsGameUnderTest.State = new GameInProgressState(CrazyEightsGameUnderTest);

            // Playing eight skips the next player
            CrazyEightsGameUnderTest.TakeTurn(CrazyEightsGameUnderTest.Players.Players[0], new Card(topCard.Suit, Card.Values.Eight));
            Assert.Equal(CrazyEightsGameUnderTest.Players.Players[0], CrazyEightsGameUnderTest.Players.GetPlayer());
            
            // Playing two adds two card to the next player
            CrazyEightsGameUnderTest.TakeTurn(CrazyEightsGameUnderTest.Players.GetPlayer(), new Card(topCard.Suit, Card.Values.Two));
            Assert.True(4 == CrazyEightsGameUnderTest.Players.Players[1].ShowHand().Count);

            // Played card is not possible on discard pile (second player at play)
            CrazyEightsGameUnderTest.TakeTurn(CrazyEightsGameUnderTest.Players.GetPlayer(),
                new Card(Card.Suits.Hearts, Card.Values.Ace));
            Assert.Equal(CrazyEightsGameUnderTest.Players.Players[1], CrazyEightsGameUnderTest.Players.GetPlayer());

            // Played card not in possession (turn sticks)
            CrazyEightsGameUnderTest.TakeTurn(
                CrazyEightsGameUnderTest.Players.GetPlayer(), new Card(Card.Suits.Diamonds, Card.Values.Jack));
            Assert.Equal(CrazyEightsGameUnderTest.Players.Players[1], CrazyEightsGameUnderTest.Players.GetPlayer());

            // Unknown player 
            Assert.Throws<InvalidOperationException>(() => CrazyEightsGameUnderTest.TakeTurn(
                new Player("Test3"), new Card(Card.Suits.Hearts, Card.Values.Seven)));
        }

        [Fact]
        public void Stay()
        {
            
            var CrazyEightsGameUnderTest = new CrazyEightsGame();
            CrazyEightsGameUnderTest.AddPlayer(new Player("Test1"));
            CrazyEightsGameUnderTest.AddPlayer(new Player("Test2"));
                        
            CrazyEightsGameUnderTest.Players.Players[0].GiveHand(new List<Card> { 
                new Card(Card.Suits.Clubs, Card.Values.Eight), 
                new Card(Card.Suits.Hearts, Card.Values.Two) 
            });

            CrazyEightsGameUnderTest.State = new GameInProgressState(CrazyEightsGameUnderTest);
            CrazyEightsGameUnderTest.SkipTurn(CrazyEightsGameUnderTest.Players.GetPlayer());
            Assert.True(3 == CrazyEightsGameUnderTest.Players.Players[0].ShowHand().Count);
            Assert.Equal(new Player("Test2"), CrazyEightsGameUnderTest.Players.GetPlayer());
        }
    }
}

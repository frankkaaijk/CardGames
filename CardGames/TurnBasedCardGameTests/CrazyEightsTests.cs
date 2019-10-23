using CardGames;
using System;
using System.Collections.Generic;
using Xunit;

namespace CardGameTests
{
    public class CrazyEightsTests
    {
        SheddingCardGames.CrazyEightsGame CrazyEightsGameUnderTest = new SheddingCardGames.CrazyEightsGame();
        public CrazyEightsTests()
        {

        }
        [Fact]
        public void Hit()
        {
            var player1 = new Player("Test1");
            var topCard = CrazyEightsGameUnderTest.ShowTopOfDeck();
            player1.GiveHand(new List<Card> {
                new Card(topCard.Suit, Card.Values.Eight),
                new Card(topCard.Suit, Card.Values.Two),
                new Card(Card.Suits.Hearts, Card.Values.Four) });
            var player2 = new Player("Test2");
            player2.GiveHand(new List<Card> {
                new Card(Card.Suits.Clubs, Card.Values.Six),
                new Card(Card.Suits.Spades, Card.Values.Three) });

            var result = CrazyEightsGameUnderTest.Hit(new Card(topCard.Suit, Card.Values.Eight), ref player1, ref player2);
            Assert.True(NextMove.SkipNextPlayer == result);
            result = CrazyEightsGameUnderTest.Hit(new Card(topCard.Suit, Card.Values.Two), ref player1, ref player2);
            Assert.True(NextMove.NextPlayer == result);
        }

        [Fact]
        public void Stay()
        {
            var player1 = new Player("Test1");
            player1.GiveHand(new List<Card> { new Card(Card.Suits.Clubs, Card.Values.Eight), new Card(Card.Suits.Hearts, Card.Values.Two) });

            var result = CrazyEightsGameUnderTest.Stay(ref player1);
            Assert.True(NextMove.NextPlayer == result);
            var count = player1.ShowHand().Split(Environment.NewLine).Length;
            Assert.True(3 == count);
        }

        [Fact]
        public void StateWon()
        {
            CrazyEightsGameUnderTest.SetState(GameStates.Won);
            var player1 = new Player("Test1");
            var player2 = new Player("Test2");
            Assert.Throws<NotSupportedException>(() => CrazyEightsGameUnderTest.Stay(ref player1));
            Assert.Throws<NotSupportedException>(() => CrazyEightsGameUnderTest.Hit(null, ref player1, ref player2));
            Assert.Throws<NotSupportedException>(() => CrazyEightsGameUnderTest.ShowTopOfDeck());
            Assert.Throws<NotSupportedException>(() => CrazyEightsGameUnderTest.DealHand());

            Assert.Throws<NotSupportedException>(() => CrazyEightsGameUnderTest.SetState(GameStates.Playing));
        }
        [Fact]
        public void FlipDecks()
        {
            // First draw almost all cards from the deck.
            var crazyEightsPlayingUnderTest = new CrazyEightsGame.States.CrazyEightsPlaying(CrazyEightsGameUnderTest);
            var playedCards = crazyEightsPlayingUnderTest.PlayDeck.DealHand(53);

            // Force these cards to the discard deck as if they were played.
            foreach(var card in playedCards)
            {
                crazyEightsPlayingUnderTest.DiscardDeck.AddCard(card);
            }
            
            // Now try and draw four more cards
            crazyEightsPlayingUnderTest.DealHand();

            // Deck should be resetted (DealHand has taken 4 cards so card count is 50, not 54)
            Assert.True(crazyEightsPlayingUnderTest.PlayDeck.Cards.Count == 50);
            Assert.True(crazyEightsPlayingUnderTest.DiscardDeck.Cards.Count == 1);
        }
    }
}

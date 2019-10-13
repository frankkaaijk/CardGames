using System;
using TurnBasedCardGame;
using Xunit;
using static TurnBasedCardGame.Card;
using static TurnBasedCardGame.Deck;

namespace TurnBasedCardGameTests
{
    public class PlayerTests : IDisposable
    {
        private Deck deck;
        private Player player1, player2;

        public PlayerTests()
        {
            deck = new Deck(DeckType.StandardWithJokers);  // Don't shuffle the deck so we can assume the dealt hands
            player1 = new Player("Test1");
            player2 = new Player("Test2");

            // Ace of Hearts, Two of Hearts, Three of Hearts, Four of Harts
            player1.GiveHand(deck.DealHand(4));
            // Five of Hearts, Six of Hearts, Seven Of Hearts, Eight of Harts,
            // Nine of Hearts, Ten of Hearts, Jack of Hearts, Queen of Harts
            player2.GiveHand(deck.DealHand(8));
        }

        [Fact]
        public void DealHand()
        {
            Assert.Equal(4, player1.Hand.Count);
            Assert.Contains(new Card(Suits.Clubs, Values.King), player1.Hand);
            Assert.DoesNotContain(new Card(Suits.Diamonds, Values.Ace), player1.Hand);
            Assert.Equal(8, player2.Hand.Count);
            Assert.Contains(new Card(Suits.Clubs, Values.Eight), player2.Hand);
            Assert.DoesNotContain(new Card(Suits.Hearts, Values.Ace), player2.Hand);
        }

        [Fact]
        public void DisplayHand()
        {
            Assert.Contains("King of Clubs", player1.ShowHand());
            Assert.Contains("Eight of Clubs", player2.ShowHand());
        }


        public void Dispose()
        {
            // no implementation
        }
    }
}

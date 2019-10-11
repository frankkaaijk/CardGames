using System;
using System.Collections.Generic;
using System.Text;
using TurnBasedCardGame;
using Xunit;
using static TurnBasedCardGame.Card;

namespace TurnBasedCardGameTests
{
    public class PlayerTests 
    {
        private Deck deck;
        private Player player1, player2;

        public PlayerTests()
        {
            deck = new Deck();  // Don't shuffle the deck so we can assume the dealt hands
            deck.Initialize();
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
            Assert.Contains(new Card(Suits.Hearts, Values.Four), player1.Hand);
            Assert.DoesNotContain(new Card(Suits.Clubs, Values.Ace), player1.Hand);
            Assert.Equal(8, player2.Hand.Count);
            Assert.Contains(new Card(Suits.Hearts, Values.Queen), player2.Hand);
            Assert.DoesNotContain(new Card(Suits.Diamonds, Values.Ace), player2.Hand);
        }

        [Fact]
        public void DisplayHand()
        {
            Assert.Contains("Ace of Hearts", player1.ShowHand());
            Assert.Contains("Queen of Hearts", player2.ShowHand());
        }

        [Fact]
        public void RemoveFromHand()
        {
            // Remove unknown card
            Assert.Throws<InvalidOperationException>(()=>player1.RemoveCardFromHand(new Card(Suits.Diamonds, Values.Queen)));
            player1.RemoveCardFromHand(new Card(Suits.Hearts, Values.Three));
            Assert.DoesNotContain(new Card(Suits.Hearts, Values.Three), player1.Hand);
            Assert.True(3 == player1.Hand.Count);
                        
            player2.RemoveCardFromHand(new Card(Suits.Hearts, Values.Jack));
            Assert.DoesNotContain(new Card(Suits.Hearts, Values.Three), player1.Hand);
            Assert.True(7 == player2.Hand.Count);
        }

        [Fact]
        public void AddCardToHand()
        {
            player1.AddCardToHand(new Card(Suits.Diamonds, Values.Three));
            Assert.Contains(new Card(Suits.Diamonds, Values.Three), player1.Hand);
            Assert.True(5 == player1.Hand.Count);

            player2.AddCardToHand(new Card(Suits.Clubs, Values.Five));
            Assert.Contains(new Card(Suits.Clubs, Values.Five), player2.Hand);
            Assert.True(9 == player2.Hand.Count);
        }
    }
}

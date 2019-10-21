using System;
using CardGames;
using Xunit;
using static CardGames.Card;
using static CardGames.Deck;

namespace CardGameTests
{
    public class PlayerTests 
    {
        private Deck deck;
        private Player player1, player2;

        public PlayerTests()
        {
            deck = new Deck(DeckType.FrenchIncludingJokers);  // Don't shuffle the deck so we can assume the dealt hands
            player1 = new Player("Test1");
            player2 = new Player("Test2");

            // Joker (x2), King of Clubs, Queen of Clubs
            player1.GiveHand(deck.DealHand(4));
            // Jack of Clubs, Ten of Clubs, Nine of Clubs, Eight of Clubs, 
            // Seven of Clubs, six of Clubs, Five of Clubs, Four of Clubs
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

        [Fact]
        public void RemoveFromHand()
        {
            // Remove unknown card
            Assert.Throws<InvalidOperationException>(()=>player1.RemoveFromHand(new Card(Suits.Diamonds, Values.Queen)));
            player1.RemoveFromHand(new Card(Suits.Clubs, Values.King));
            Assert.DoesNotContain(new Card(Suits.Clubs, Values.King), player1.Hand);
            Assert.True(3 == player1.Hand.Count);
                        
            player2.RemoveFromHand(new Card(Suits.Clubs, Values.Eight));
            Assert.DoesNotContain(new Card(Suits.Clubs, Values.Eight), player1.Hand);
            Assert.True(7 == player2.Hand.Count);
        }

        [Fact]
        public void AddCardToHand()
        {
            player1.AddToHand(new Card(Suits.Diamonds, Values.Three));
            Assert.Contains(new Card(Suits.Diamonds, Values.Three), player1.Hand);
            Assert.True(5 == player1.Hand.Count);

            player2.AddToHand(new Card(Suits.Clubs, Values.Five));
            Assert.Contains(new Card(Suits.Clubs, Values.Five), player2.Hand);
            Assert.True(9 == player2.Hand.Count);
        }
    }
}

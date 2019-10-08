using System;
using Xunit;
using TurnBasedCardGame;

namespace TurnBasedCardGameTests
{
    public class DeckTests
    {
        [Fact]
        public void CreateDeck()
        {
            var deck = new Deck();
            // 54 cards includes two jokers for each deck
            Assert.Equal(54, deck.Cards.Count);
        }

        [Fact]
        public void ShuffleDeck()
        {
            var shuffledDeck = new Deck();
            var unshuffledDeck = new Deck();
            shuffledDeck.Shuffle();

            Assert.NotSame(shuffledDeck, unshuffledDeck);
        }

        [Fact]
        public void DealHand()
        {
            var deck = new Deck();
            Assert.Equal(4, deck.DealHand(4).Count);
            Assert.Equal(8, deck.DealHand(8).Count);
            Assert.Equal(42, deck.Cards.Count);
        }
    }

}

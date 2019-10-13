using System;
using Xunit;
using TurnBasedCardGame;
using static TurnBasedCardGame.Deck;

namespace TurnBasedCardGameTests
{
    public class DeckTests
    {
        [Fact]
        public void CreateDeck()
        {
            var deck = new Deck(DeckType.StandardWithJokers);
            // 54 cards includes two jokers for each deck
            Assert.Equal(54, deck.Cards.Count);
        }

        [Fact]
        public void ShuffleDeck()
        {
            var shuffledDeck = new Deck(DeckType.StandardWithJokers);
            var unshuffledDeck = new Deck(DeckType.StandardWithJokers);
            shuffledDeck.Shuffle();

            Assert.NotSame(shuffledDeck, unshuffledDeck);
        }

        [Fact]
        public void DealHand()
        {
            var deck = new Deck(DeckType.StandardWithJokers);
            Assert.Equal(4, deck.DealHand(4).Count);
            Assert.Equal(8, deck.DealHand(8).Count);
            Assert.Equal(42, deck.Cards.Count);
        }
    }

}

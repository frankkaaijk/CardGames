using System;
using Xunit;
using TurnBasedCardGame;
using static TurnBasedCardGame.Deck;

namespace CardGameTests
{
    public class DeckTests
    {
        [Fact]
        public void CreateDeck()
        {
            var deck = new Deck(DeckType.FrenchIncludingJokers);
            // 54 cards includes two jokers for each deck
            Assert.Equal(54, deck.Cards.Count);
        }

        [Fact]
        public void ShuffleDeck()
        {
            var shuffledDeck = new Deck(DeckType.FrenchIncludingJokers);
            var unshuffledDeck = new Deck(DeckType.FrenchIncludingJokers);
            shuffledDeck.Shuffle();

            Assert.NotSame(shuffledDeck, unshuffledDeck);
        }

        [Fact]
        public void DealHand()
        {
            var deck = new Deck(DeckType.FrenchIncludingJokers);
            Assert.Equal(4, deck.DealHand(4).Count);
            Assert.Equal(8, deck.DealHand(8).Count);
            Assert.Equal(42, deck.Cards.Count);
            deck.Cards.Clear();
            deck.AddCard(new Card(Card.Suits.Spades, Card.Values.Five));
            Assert.Throws<InvalidOperationException>(()=>deck.DealHand(5));
        }

        [Fact]
        public void AddCard()
        {
            var deck = new Deck(DeckType.Empty);
            Assert.Empty(deck.Cards);
            deck.AddCard(new Card(Card.Suits.Clubs, Card.Values.Jack));
            Assert.Single(deck.Cards);
            deck.AddCard(new Card(Card.Suits.Diamonds, Card.Values.Four));
            Assert.Equal(2, deck.Cards.Count);            
        }
    }

}

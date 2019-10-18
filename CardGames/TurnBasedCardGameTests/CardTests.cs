using TurnBasedCardGame;
using Xunit;

namespace CardGameTests
{
    public class CardTests
    {
        [Theory]
        [InlineData("King of Hearts")]
        [InlineData("kING oF heArts")]
        [InlineData("jOKER")]
        [InlineData("Queen of CLubs")]
        [InlineData("Jack of Spades")]
        public void ParseValid(string cardString)
        {
            Card parsedCard;
            Card.TryParse(cardString, out parsedCard);
            Assert.NotNull(parsedCard);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Hearts of King")]
        [InlineData("SOmeThing")]        
        public void ParseInvalid(string cardString)
        {
            Card parsedCard;
            Card.TryParse(cardString, out parsedCard);
            Assert.Null(parsedCard);
        }
    }
}

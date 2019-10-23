using System;
using CardGames;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace CrazyEightsConsole
{
    public static class CollectionExtensions
    {
        public static string MyToString<TKey, TValue> (this Dictionary<TKey, TValue> dict)
        {
            return string.Join(Environment.NewLine ,dict.Select(kv => kv.Key + ") " + kv.Value).ToArray());
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Crazy Eights");
            Console.WriteLine("============");
            Console.Write("How many players?");
            Int32.TryParse(Console.ReadLine(), out var numberOfPlayers);

            var crazyEightGame = new SheddingCardGames.CrazyEightsGame();

            var crazyEightsGame = new CardGames.CardGame(crazyEightGame);
            SetupPlayers(crazyEightsGame, numberOfPlayers);
            crazyEightsGame.StartGame();
            
            while (true)
            {
                Console.WriteLine("Top of Crazy Eights deck: " + crazyEightsGame.ShowLastPlayedCard());

                string currentPlayer = crazyEightsGame.ShowCurrentPlayer();
                Console.WriteLine(currentPlayer + " has: ");
                var cards = CardSelection(crazyEightsGame);
                Console.WriteLine(cards.MyToString());
                Console.WriteLine("Hit (1 to 9) or Stay (0)?");
                int play = 0;
                int.TryParse(Console.ReadLine(), out play);
                if (play == 0)
                {
                    crazyEightsGame.Stay();
                    continue;
                }
                crazyEightsGame.Hit(cards[play]);
            }
        }

        private static void SetupPlayers(CardGames.CardGame cardGame, int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var inputString = String.Format("Name player name {0}:", i + 1);
                Console.Write(inputString);
                var playerName = Console.ReadLine();
                while (false == cardGame.AddPlayer(playerName))
                {
                    Console.WriteLine($"Player with name {playerName} already registered");
                    Console.Write(inputString);
                    playerName = Console.ReadLine();
                }
            }
        }

        private static Dictionary<int, string> CardSelection(CardGames.CardGame crazyEightsGame)
        {
            var cards = crazyEightsGame.ShowPlayersHand(crazyEightsGame.ShowCurrentPlayer());
            var expr = Environment.NewLine;
            var splitCards = Regex.Split(cards, expr);
            var myCards = new Dictionary<int, string>();

            int cardCounter = 0;

            foreach (var card in splitCards)
            {   
                myCards.Add(++cardCounter, card);
            }
            return myCards;
        }
    }
}

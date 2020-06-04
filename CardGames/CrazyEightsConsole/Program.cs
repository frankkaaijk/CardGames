using System;
using CardGames;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using CrazyEightsGame;

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

            var crazyEightsPlayers = new CrazyEightsPlayers();
            SetupPlayers(crazyEightsPlayers, numberOfPlayers);

            var crazyEightsGame = new SheddingCardGames.CrazyEightsGame(crazyEightsPlayers);
            crazyEightsGame.DealHands();
                        
            while (true)
            {
                Console.WriteLine("Top of Crazy Eights deck: " + crazyEightsGame.TopOfDeck());

                Player currentPlayer = crazyEightsPlayers.GetPlayer();
                Console.WriteLine(currentPlayer.Name + " has: ");
                var cards = CardSelection(currentPlayer);
                Console.WriteLine(cards.MyToString());
                Console.WriteLine("Hit (1 to 9) or Stay (0)?");
                int.TryParse(Console.ReadLine(), out var play);
                if (play == 0)
                {
                    crazyEightsGame.SkipTurn(crazyEightsPlayers.GetPlayer());
                    continue;
                }
                crazyEightsGame.TakeTurn(currentPlayer, currentPlayer.ShowHand().ElementAt(play-1));
            }
        }

        private static void SetupPlayers(CrazyEightsPlayers players, int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var inputString = String.Format("Name player name {0}:", i + 1);
                Console.Write(inputString);
                var playerName = Console.ReadLine();
                try
                {
                    players.AddPlayer(new Player(playerName));
                }
                catch( NotImplementedException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(inputString);
                    playerName = Console.ReadLine();
                }
            }
        }

        private static Dictionary<int, string> CardSelection(Player currentPlayer)
        {
            var myCards = new Dictionary<int, string>();

            int cardCounter = 0;

            foreach (var card in currentPlayer.ShowHand())
            {   
                myCards.Add(++cardCounter, card.ToString());
            }
            return myCards;
        }
    }
}

using System;
using TurnBasedCardGame;

namespace CrazyEightsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Crazy Eights");
            Console.WriteLine("How many players?");
            Int32.TryParse(Console.ReadLine(), out var numberOfPlayers);

            var crazyEightsGame = new TurnBasedCardGame.CardGame();
            SetupPlayers(crazyEightsGame, numberOfPlayers);
            crazyEightsGame.StartGame();
            Console.WriteLine("Begin Crazy Eights with: " + crazyEightsGame.ShowLastPlayedCard());

            Console.Write(crazyEightsGame.ShowPlayersHand("Frank"));
            while (true)
            {
                crazyEightsGame.Hit("Frank", Console.ReadLine());
                Console.Write(crazyEightsGame.ShowPlayersHand("Frank"));

                Console.WriteLine("Continue Crazy Eights with: " + crazyEightsGame.ShowLastPlayedCard());
            }
        }

        private static void SetupPlayers(CardGame cardGame, int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var inputString = String.Format("Name player name {0}:", i+1);
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
    }
}

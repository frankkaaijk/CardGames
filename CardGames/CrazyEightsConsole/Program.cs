using System;
using TurnBasedCardGame;

namespace CrazyEightsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Crazy Eights");
            Console.WriteLine("============");
            Console.Write("How many players?");
            Int32.TryParse(Console.ReadLine(), out var numberOfPlayers);

            var crazyEightGame = new SheddingCardGames.CrazyEightGame();

            var crazyEightsGame = new CardGame(crazyEightGame);
            SetupPlayers(crazyEightsGame, numberOfPlayers);
            crazyEightsGame.StartGame();
            Console.WriteLine("Begin Crazy Eights with: " + crazyEightsGame.ShowLastPlayedCard());

            while (true)
            {
                string currentPlayer = crazyEightsGame.ShowCurrentPlayer();
                Console.WriteLine(crazyEightsGame.ShowPlayersHand(currentPlayer));
                Console.WriteLine ("Hit (1) or Stay (2)?");
                int play;
                int.TryParse(Console.ReadLine(), out play);
                if (play == 1)
                {
                    Console.Write("Card to play:");
                    crazyEightsGame.Hit(Console.ReadLine());
                }
                else if (play == 2)
                {
                    crazyEightsGame.Stay();
                }

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

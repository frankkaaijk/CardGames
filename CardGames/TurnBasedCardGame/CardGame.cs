using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CardGameTests")]
namespace CardGames
{
    public enum NextMove
    {
        CurrentPlayer,
        NextPlayer,
        SkipNextPlayer,
        ReservePlayOrder,
        GameWon
    }
    public class CardGame
    {
        internal LinkedList<Player> Players = new LinkedList<Player>();
        internal Player CurrentPlayer;
        internal ICardGameCommands TypeOfCardgame;
        internal PlayDirection DirectionOfPlay = PlayDirection.Clockwise;

        internal enum PlayDirection
        {
            Clockwise,
            CounterClockwise
        }

        public CardGame(ICardGameCommands cardgame)
        {
            TypeOfCardgame = cardgame;
        }

        public bool AddPlayer(string playername)
        {
            foreach (var player in Players)
            {
                if (player.Name == playername)
                {
                    return false;
                }
            }

            var newPlayer = new Player(playername);
            //newPlayer.GiveHand(TypeOfCardgame.DealHand());
            Players.AddLast(newPlayer);

            return true;
        }
        public string ShowCurrentPlayer()
        {
            return CurrentPlayer.Name;
        }
        public string ShowLastPlayedCard()
        {
            return TypeOfCardgame.ShowTopOfDeck().ToString();
        }

        public string ShowPlayersHand(string playername)
        {
            var player = Players.FirstOrDefault(x => x.Name == playername);
            return player.ShowHand();
        }

        public void StartGame()
        {
            foreach(var player in Players)
            {
                player.GiveHand(TypeOfCardgame.DealHand());
            }
            TypeOfCardgame.SetState(GameStates.Playing);
            CurrentPlayer = Players.First();
        }

        public void Hit(string cardString)
        {
            Card cardToPlay;
            if (!Card.TryParse(cardString, out cardToPlay))
            {
                throw new FormatException("Invalid (unknown) card");
            }

            var nextPlayer = GetNextPlayer();
            var nextMove = TypeOfCardgame.Hit(cardToPlay, ref CurrentPlayer, ref nextPlayer);
            ProgressPlay(nextMove);
        }
        public void Stay()
        {
            TypeOfCardgame.Stay(ref CurrentPlayer);
            ProgressPlay(NextMove.NextPlayer);
        }

        internal void ProgressPlay(NextMove nextMove)
        {
            switch (nextMove)
            {
                case NextMove.ReservePlayOrder:
                    {
                        var currentDirection = DirectionOfPlay;
                        DirectionOfPlay = currentDirection ==
                            PlayDirection.Clockwise ? PlayDirection.CounterClockwise : PlayDirection.Clockwise;
                        CurrentPlayer = GetNextPlayer();
                        break;
                    }
                case NextMove.CurrentPlayer:
                    {
                        // Do not touch the current player
                        break;
                    }
                case NextMove.NextPlayer:
                    {
                        CurrentPlayer = GetNextPlayer();
                        break;
                    }
                case NextMove.SkipNextPlayer:
                    {
                        CurrentPlayer = GetNextPlayer();
                        CurrentPlayer = GetNextPlayer();
                        break;
                    }
                case NextMove.GameWon:
                    {
                        TypeOfCardgame.SetState(GameStates.Won);
                        break;
                    }
                default:
                    {
                        throw new IndexOutOfRangeException("Invalid NextMove");
                    }
            }
        }

        private Player GetNextPlayer()
        {
            var currentPlayerNode = Players.Find(CurrentPlayer);
            if (DirectionOfPlay == PlayDirection.Clockwise)
            {
                return (currentPlayerNode.Next != null) ?
                    currentPlayerNode.Next.Value : Players.First.Value;
            }

            return (currentPlayerNode.Previous != null) ?
                currentPlayerNode.Previous.Value : Players.Last.Value;

        }
    }
}

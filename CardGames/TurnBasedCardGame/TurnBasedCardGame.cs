using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TurnBasedCardGameTests")]
namespace TurnBasedCardGame
{
    public enum NextMove
    {
        CurrentPlayer,
        NextPlayer,
        SkipNextPlayer,
        ReservePlayOrder
    }
    public class CardGame
    {
        internal LinkedList<Player> Players = new LinkedList<Player>();
        internal Player CurrentPlayer;
        internal bool GameInProgress = false;   // state pattern?
        internal ICardGame TypeOfCardgame;
        internal PlayDirection DirectionOfPlay = PlayDirection.Clockwise;

        internal enum PlayDirection
        {
            Clockwise,
            CounterClockwise
        }

        public CardGame(ICardGame cardgame)
        {
            TypeOfCardgame = cardgame;
        }

        public bool AddPlayer(string playername)
        {
            if (GameInProgress)
            {
                throw new InvalidOperationException("Game already in progress");
            }
            foreach (var player in Players)
            {
                if (player.Name == playername)
                {
                    return false;
                }
            }

            var newPlayer = new Player(playername);
            TypeOfCardgame.DealHand(ref newPlayer);
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
            GameInProgress = true;
            CurrentPlayer = Players.First();
        }

        public void Hit(string cardString)
        {
            if (!GameInProgress)
            {
                throw new InvalidOperationException("Game not in progress");
            }

            Card cardToPlay;
            if (!Card.TryParse(cardString, out cardToPlay))
            {
                throw new FormatException("Invalid (unknown) card");
            }

            var nextMove = TypeOfCardgame.Hit(cardToPlay, CurrentPlayer, GetNextPlayer());
            ProgressPlay(nextMove);
        }
        public void Stay()
        {
            TypeOfCardgame.Stay(CurrentPlayer);
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

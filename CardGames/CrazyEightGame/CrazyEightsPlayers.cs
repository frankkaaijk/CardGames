using CardGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CrazyEightsGame
{
    public enum PlayDirection
    {
        Clockwise,
        CounterClockWise
    }

    public class CrazyEightsPlayers : IEnumerable

    {
        internal List<Player> Players = new List<Player>();
        public PlayDirection PlayDirection { get; set; }
        internal int CurrentPlayer = 0;
        public int Count { get { return Players.Count; }
            private set { }
        }
                                        
        public void AddPlayer(Player player)
        {
            if (Players.Count > 5)
            {
                throw new NotImplementedException("Only five players can join");
            }
                        
            if( Players.Contains(player))
            {
                throw new NotImplementedException("Player already in the game");
            }
                        
            Players.Add(player);
        }

        public void NextPlayer()
        {
            if( PlayDirection == PlayDirection.Clockwise )
            {
                if( ++CurrentPlayer > Players.Count - 1)
                {
                    // Last item in the list, start from the front
                    CurrentPlayer = 0;
                }
                return;
            }            

            if (CurrentPlayer-- < 0)
            {
                // First item in the list, start from the back
                CurrentPlayer = Players.Count;
            }
        }

        public Player GetPlayer()
        {
            return Players[CurrentPlayer];
        }

        public bool Contains(Player player)
        {
            return Players.Contains(player);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Players).GetEnumerator();
        }
    }
}

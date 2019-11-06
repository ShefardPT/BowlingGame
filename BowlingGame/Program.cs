using BowlingGame.Core;
using System;

namespace BowlingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameEngine = new Engine();

            gameEngine.RunGame();
        }
    }
}

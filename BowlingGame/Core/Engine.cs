using BowlingGame.Core.Interfaces;
using BowlingGame.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Core
{
    public class Engine
    {
        public void RunGame()
        {
            NotifyGameStarted();

            IScoreBoard board = new TraditionalScoreBoard();
            IRunner gameRunner = new SimpleRunner(board);
            gameRunner.RunGame();

            NotifyGameEnded();
        }

        private void NotifyGameEnded()
        {
            Console.WriteLine("Game has been ended.");
        }

        private void NotifyGameStarted()
        {
            Console.WriteLine("Game has been started.");
        }
    }
}

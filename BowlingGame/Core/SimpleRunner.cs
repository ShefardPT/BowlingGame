using BowlingGame.Core.Interfaces;
using System;

namespace BowlingGame.Core
{
    /// <summary>
    /// Automatic rolling while rolls are allowed
    /// </summary>
    public class SimpleRunner : IRunner
    {
        private IScoreBoard _scoreBoard;

        public SimpleRunner(IScoreBoard board)
        {
            _scoreBoard = board;
        }

        public void RunGame()
        {
            var random = new Random();

            var firstRoll = 0;

            while(_scoreBoard.IsRollAllowed)
            {
                var roll = random.Next(11 - firstRoll);

                if (_scoreBoard.IsFirstFrameRoll || roll != 10)
                    firstRoll = roll;
                else
                    firstRoll = 0;                   
                
                _scoreBoard.DoRoll(roll);
                Console.WriteLine($"You have dropped {firstRoll} pin(-s)");
            }

            Console.WriteLine($"Here are your results.");
            _scoreBoard.Print();
        }
    }
}
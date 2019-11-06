using BowlingGame.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Core.Interfaces
{
    public interface IScoreBoard
    {
        List<ScoreFrame> Frames { get;}
        List<int> Scores { get; }
        bool IsRollAllowed { get; }
        int FrameIndex { get; }
        bool IsFirstFrameRoll { get; }

        /// <summary>
        /// Do roll and add its result to the board.
        /// </summary>
        /// <param name="points">Number of points in result of roll.</param>
        void DoRoll(int points);
        void Reset();
        void Print();
    }
}

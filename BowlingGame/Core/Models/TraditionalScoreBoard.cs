using BowlingGame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingGame.Core.Models
{
    public class TraditionalScoreBoard : IScoreBoard
    {
        private int _rollIndex;
        private int[] _addScoreMap;

        public int FrameIndex { get => this._rollIndex / 2; }
        public bool IsFirstFrameRoll { get => this._rollIndex % 2 == 0; }

        public List<ScoreFrame> Frames { get; private set; }
        public List<int> Scores { get; private set; }
        public bool IsRollAllowed
        {
            get
            {
                if (this._rollIndex < 20)
                    return true;

                if (this._rollIndex == 20 && this.Frames[9].State != ScoreFrame.FrameState.Open)
                    return true;

                if (this._rollIndex == 21 &&
                    this.Frames[9].State == ScoreFrame.FrameState.Strike &&
                    this.Frames[10].State == ScoreFrame.FrameState.Open)
                    return true;

                if (this._rollIndex == 22 &&
                    this.Frames[9].State == ScoreFrame.FrameState.Strike &&
                    this.Frames[10].State == ScoreFrame.FrameState.Strike)
                    return true;

                return false;
            }
        }

        public TraditionalScoreBoard()
        {
            this.Reset();
        }

        public void Reset()
        {
            var frames = new ScoreFrame[12];
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = new ScoreFrame();
            }

            this.Frames = new List<ScoreFrame>(frames);

            var scores = new int[10];
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = 0;
            }
            this.Scores = new List<int>(scores);

            this._rollIndex = 0;
            this._addScoreMap = new int[10];
            for (int i = 0; i < this._addScoreMap.Length; i++)
            {
                this._addScoreMap[i] = 0;
            }
        }

        public void DoRoll(int points)
        {
            if (!IsRollAllowed)
                throw new InvalidOperationException("Roll is not allowed.");

            var isFrameOne = this.IsFirstFrameRoll;
            var frameIndex = this.FrameIndex;

            AddPointsToAwaitingFrames(points);

            try
            {
                AddPointsToCurrentFrame(points, frameIndex, isFrameOne);
            }
            catch (Exception)
            {
                throw;
            }

            if (this.Frames[frameIndex].State != ScoreFrame.FrameState.Open)
            {
                RegisterFrameAsAwaiting(frameIndex, this.Frames[frameIndex].State);
            }
        }

        private void RegisterFrameAsAwaiting(int frameIndex, ScoreFrame.FrameState state)
        {
            var rollsCount = 0;
            if (state == ScoreFrame.FrameState.Strike)
                rollsCount = 2;
            else if (state == ScoreFrame.FrameState.Spare)
                rollsCount = 1;

            if (frameIndex < 10)
                this._addScoreMap[frameIndex] = rollsCount;
        }

        /// <summary>
        /// Adds points to the current frame
        /// </summary>
        /// <param name="points"></param>
        /// <param name="frameIndex"></param>
        private void AddPointsToCurrentFrame(int points, int frameIndex, bool isFrameOne)
        {
            var frame = Frames[frameIndex];
            if (isFrameOne)
            {
                frame.FrameOne = points;

                if (frame.State == ScoreFrame.FrameState.Strike)
                    this._rollIndex++;
            }
            else
            {
                frame.FrameTwo = points;
            }

            if (frameIndex < 10)
                this.Scores[frameIndex] += points;

            _rollIndex++;
        }

        /// <summary>
        /// Adds points to previous "strike" and "spare" frames.
        /// </summary>
        /// <param name="points"></param>
        private void AddPointsToAwaitingFrames(int points)
        {
            for (int i = 0; i < this._addScoreMap.Length; i++)
            {
                if (this._addScoreMap[i] == 0)
                    continue;

                this.Scores[i] += points;
                this._addScoreMap[i]--;
            }
        }

        public void Print()
        {
            Console.Write("Frame:");
            for (int i = 0; i < this.Frames.Count; i++)
            {
                Console.Write($"\t {i + 1}");
            }
            Console.WriteLine();

            Console.Write("Scores:");
            foreach (var frame in this.Frames)
            {
                Console.Write("\t");

                switch (frame.State)
                {
                    case ScoreFrame.FrameState.Open:
                        Console.Write($"{frame.FrameOne}|{frame.FrameTwo}");
                        break;
                    case ScoreFrame.FrameState.Spare:
                        Console.Write($"{frame.FrameOne}|/");
                        break;
                    case ScoreFrame.FrameState.Strike:
                        Console.Write("X|-");
                        break;
                    default:
                        throw new Exception("Unexpected frame condition.");
                }
            }
            Console.WriteLine();

            foreach (var score in this.Scores)
            {
                Console.Write("\t");
                Console.Write($"{score}");
            }
            var total = this.Scores.Sum();
            Console.Write($"\tTotal: {total}");
            Console.WriteLine();
        }
    }
}

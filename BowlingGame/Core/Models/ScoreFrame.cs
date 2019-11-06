using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Core.Models
{
    /// <summary>
    /// Contains info about score during a frame
    /// </summary>
    public class ScoreFrame
    {
        public enum FrameState
        {
            Open,
            Spare,
            Strike
        }

        private int[] _frame;

        public FrameState State
        {
            get
            {
                if (_frame[0] + _frame[1] == 10)
                {
                    if (_frame[0] == 10)
                        return FrameState.Strike;
                    else
                        return FrameState.Spare;
                }
                else
                    return FrameState.Open;
            }
        }

        public int FrameOne 
        { 
            get => _frame[0];
            set 
            {
                if(value < 0)
                    throw new ArgumentException("Value of frame points cannot be negative.");
                if (this.FrameTwo + value > 10)
                    throw new ArgumentException("Sum of frames points cannot be more than 10.");
                _frame[0] = value;
            }
        }

        public int FrameTwo
        {
            get => _frame[1];
            set
            {
                if (value < 0)
                    throw new ArgumentException("Value of frame points cannot be negative.");
                if (this.FrameOne + value > 10)
                    throw new ArgumentException("Sum of frames points cannot be more than 10.");
                _frame[1] = value;
            }
        }

        public ScoreFrame()
        {
            this._frame = new int[2] { 0, 0 };
        }
    }
}

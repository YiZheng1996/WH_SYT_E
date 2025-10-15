using System;
using System.Collections.Generic;
using System.Text;

namespace MainUI.Procedure
{
    public class SubmitArgs : EventArgs
    {
        public SubmitArgs()
        {
            this.Gain = 1;
        }

        public SubmitArgs(int index, double gain, double zero)
        {
            this.Index = index;
            this.Gain = gain;
            this.Zero = zero;
        }

        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }


        private double gain;

        public double Gain
        {
            get { return gain; }
            set { gain = value; }
        }

        private double zero;

        public double Zero
        {
            get { return zero; }
            set { zero = value; }
        }

    }
}

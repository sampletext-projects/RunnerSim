using System;

namespace RunnerSim
{
    public class BarrierRunner : Runner
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public BarrierRunner(float speed) : base(speed)
        {
        }
    }
}
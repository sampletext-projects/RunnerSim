using System;
using System.Drawing;

namespace RunnerSim
{
    // Бегун с барьером
    public class BarrierRunner : Runner
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public BarrierRunner(float speed) : base(speed)
        {
        }

        public override void Draw(Graphics g, int stadiumOffsetY, int i, float trackStart, float trackEnd, int trackHeight, int trackMargin, Image[] runnerImages)
        {
            int runnerY = stadiumOffsetY + i * (trackHeight + trackMargin);

            g.FillRectangle(Brushes.White, trackStart + (trackEnd - trackStart) * 0.33f - 30, stadiumOffsetY + i * (trackHeight + trackMargin) + 3f, 5, 24);
            g.FillRectangle(Brushes.White, trackStart + (trackEnd - trackStart) * 0.66f - 30, stadiumOffsetY + i * (trackHeight + trackMargin) + 3f, 5, 24);

            float distanceToBarrier;
            if ((distanceToBarrier = MathF.Abs(CurrentPosition - 0.33f)) < 0.05f)
            {
                // График получается такой \/, поэтому мы его конвертируем (умножаем на -1 и добавляем 1), получая /\, как раз как надо
                runnerY -= (int)((distanceToBarrier * -1 + 1) * 20);
            }

            if ((distanceToBarrier = MathF.Abs(CurrentPosition - 0.66f)) < 0.05f)
            {
                runnerY -= (int)((distanceToBarrier * -1 + 1) * 20);
            }

            g.DrawImage(runnerImages[i], trackStart + (trackEnd - trackStart) * CurrentPosition - 30, runnerY, 30, trackHeight);
        }
    }
}
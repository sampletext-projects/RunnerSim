using System;

namespace RunnerSim
{
    public class RandomlyFailingRunner : Runner
    {
        private Random _random = new Random(DateTime.Now.Millisecond);
        public bool HasFailed { get; set; }

        public RandomlyFailingRunner(float speed) : base(speed)
        {
        }

        public override void Reset()
        {
            base.Reset();
            HasFailed = false;
        }

        public override void OnRaceTick(float stadiumLength, float deltaSeconds)
        {
            base.OnRaceTick(stadiumLength, deltaSeconds);

            // с вероятность 10% бегун фэйлит забег (сходит с дистанции)
            if (_random.Next(0, 10) == 5)
            {
                HasFailed = true;
                InvokeFinished();
            }
        }

        public override string GetStats()
        {
            if (HasFailed)
            {
                return $"Бегун {Index}: Сошёл с дистанции на {ElapsedTime:F2} секунде.";
            }
            else
            {
                return $"Бегун {Index}: Время - {ElapsedTime:F2} секунд.";
            }
        }
    }
}
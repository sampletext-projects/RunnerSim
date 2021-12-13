using System;

namespace RunnerSim
{
    public class Runner
    {
        public int Index { get; set; }

        public float CurrentPosition { get; set; }

        public float Speed { get; set; }

        public bool HasFinished { get; set; }

        public float ElapsedTime { get; set; }

        public event Action<Runner> Finished;

        private Referee _referee;

        public Runner(float speed)
        {
            Speed = speed;
        }

        protected void InvokeFinished()
        {
            HasFinished =  true;
            Finished?.Invoke(this);
            Finished -= _referee.NoticeRunnerFinish;
            _referee =  null;
        }

        public virtual void Reset()
        {
            CurrentPosition = 0f;
            ElapsedTime     = 0f;
            HasFinished     = false;
        }

        public virtual void OnRaceTick(float stadiumLength, float deltaSeconds)
        {
            ElapsedTime     += deltaSeconds;
            CurrentPosition =  ((CurrentPosition * stadiumLength) + Speed * deltaSeconds) / stadiumLength;
            if (CurrentPosition > 1)
            {
                CurrentPosition = 1;
                InvokeFinished();
            }
        }

        public void OnRaceStart(Referee referee)
        {
            _referee =  referee;
            Finished += referee.NoticeRunnerFinish;
        }

        public virtual string GetStats()
        {
            return $"Бегун {Index}: Время - {ElapsedTime:F2} секунд.";
        }
    }
}
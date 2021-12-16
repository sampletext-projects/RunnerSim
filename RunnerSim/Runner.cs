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
        
        public bool HasStopped { get; set; }

        private int _stoppedTicks = 0;

        public event Action<Runner> Finished;

        private Referee _referee;

        public Runner(float speed)
        {
            Speed = speed;
        }

        protected void InvokeFinished()
        {
            HasFinished = true;
            Finished?.Invoke(this);
            Finished -= _referee.NoticeRunnerFinish;
            _referee =  null;
        }

        public virtual void Reset()
        {
            CurrentPosition = 0f;
            ElapsedTime     = 0f;
            _stoppedTicks   = 0;
            HasStopped      = false;
            HasFinished     = false;
        }

        public virtual void OnRaceTick(float stadiumLength, float deltaSeconds)
        {
            if (CurrentPosition > 1 && !HasFinished)
            {
                InvokeFinished();
            }
            else if (CurrentPosition > 1 && HasFinished)
            {
                if (_stoppedTicks < 10)
                {
                    _stoppedTicks++;
                    CurrentPosition = ((CurrentPosition * stadiumLength) + Speed / 2f * deltaSeconds) / stadiumLength;
                }
            }
            else
            {
                ElapsedTime     += deltaSeconds;
                CurrentPosition =  ((CurrentPosition * stadiumLength) + Speed * deltaSeconds) / stadiumLength;
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
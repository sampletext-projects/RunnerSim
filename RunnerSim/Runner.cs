using System;
using System.Drawing;

namespace RunnerSim
{
    // Бегун
    public class Runner
    {
        // Номер дорожки
        public int Index { get; set; }

        // Текущая позиция
        public float CurrentPosition { get; set; }

        // Скорость
        public float Speed { get; set; }

        // Финишировал ли?
        public bool HasFinished { get; set; }

        // Сколько времени бежит
        public float ElapsedTime { get; set; }

        // Остановился ли?
        public bool HasStopped { get; set; }

        // Сколько тиков таймера после финиша прошло
        private int _stoppedTicks = 0;

        // Событие финиширования
        public event Action<Runner> Finished;

        // Судья
        private Referee _referee;

        public Runner(float speed)
        {
            Speed = speed;
        }

        // Обработка финиширования
        protected void InvokeFinished()
        {
            HasFinished = true;
            Finished?.Invoke(this);
            Finished -= _referee.NoticeRunnerFinish;
            _referee =  null;
        }

        // Сброс
        public virtual void Reset()
        {
            CurrentPosition = 0f;
            ElapsedTime     = 0f;
            _stoppedTicks   = 0;
            HasStopped      = false;
            HasFinished     = false;
        }

        // Обработка тика таймера (кадра)
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


        public virtual void Draw(Graphics g, int stadiumOffsetY, int i, float trackStart, float trackEnd, int trackHeight, int trackMargin, Image[] runnerImages)
        {
            int runnerY = stadiumOffsetY + i * (trackHeight + trackMargin);

            g.DrawImage(runnerImages[i], trackStart + (trackEnd - trackStart) * CurrentPosition - 30, runnerY, 30, trackHeight);
        }

        // Событие старта гонки
        public void OnRaceStart(Referee referee)
        {
            _referee =  referee;
            Finished += referee.NoticeRunnerFinish;
        }

        // Выдача своей статистики
        public virtual string GetStats()
        {
            return $"Бегун {Index}: Время - {ElapsedTime:F2} секунд.";
        }
    }
}
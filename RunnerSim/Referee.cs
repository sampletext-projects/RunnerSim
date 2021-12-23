using System;
using System.Collections.Generic;
using System.Text;

namespace RunnerSim
{
    // Судья
    public class Referee
    {
        // список бегунов
        private List<Runner> _finishedRunners;

        // количество бегунов
        public int RunnersCount { get; set; }

        // Событие завершения гонки
        public event Action RaceFinished;
        
        // событие старта гонки
        public event Action<Referee> RaceStarted;

        public Referee()
        {
            _finishedRunners = new();
        }

        public void StartRace()
        {
            RaceStarted?.Invoke(this);
        }

        // Обработки финиша одного бегуна
        public void NoticeRunnerFinish(Runner runner)
        {
            _finishedRunners.Add(runner);
            // Console.WriteLine($"Runner {_runners.Count + 1} finished in {runner.ElapsedTime:F2} seconds");

            if (_finishedRunners.Count == RunnersCount)
            {
                RaceFinished?.Invoke();
            }
        }

        // Сброс
        public void Reset()
        {
            _finishedRunners.Clear();
            RunnersCount = 0;
        }

        // Сбор статистики
        public string GetStats()
        {
            StringBuilder builder       = new StringBuilder();
            int           index         = 1;
            for (var i = 0; i < _finishedRunners.Count; i++)
            {
                builder.AppendLine($"{index++} - {_finishedRunners[i].GetStats()}");
            }

            return builder.ToString();
        }
    }
}
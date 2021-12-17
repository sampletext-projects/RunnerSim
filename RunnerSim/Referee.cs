using System;
using System.Collections.Generic;
using System.Text;

namespace RunnerSim
{
    // Судья
    public class Referee
    {
        // список бегунов
        private List<Runner> _runners;

        // количество бегунов
        public int RunnersCount { get; set; }

        // Событие завершения гонки
        public event Action RaceFinished;

        public Referee()
        {
            _runners = new();
        }

        // Обработки финиша одного бегуна
        public void NoticeRunnerFinish(Runner runner)
        {
            _runners.Add(runner);
            // Console.WriteLine($"Runner {_runners.Count + 1} finished in {runner.ElapsedTime:F2} seconds");

            if (_runners.Count == RunnersCount)
            {
                RaceFinished?.Invoke();
            }
        }

        // Сброс
        public void Reset()
        {
            _runners.Clear();
            RunnersCount = 0;
        }

        // Сбор статистики
        public string GetStats()
        {
            StringBuilder builder       = new StringBuilder();
            int           index         = 1;
            for (var i = 0; i < _runners.Count; i++)
            {
                builder.AppendLine($"{index++} - {_runners[i].GetStats()}");
            }

            return builder.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace RunnerSim
{
    public class Referee
    {
        private List<Runner> _runners;

        public int RunnersCount { get; set; }

        public event Action RaceFinished;

        public Referee()
        {
            _runners = new();
        }

        public void NoticeRunnerFinish(Runner runner)
        {
            _runners.Add(runner);
            Console.WriteLine($"Runner {_runners.Count + 1} finished in {runner.ElapsedTime:F2} seconds");

            if (_runners.Count == RunnersCount)
            {
                RaceFinished?.Invoke();
            }
        }

        public void Reset()
        {
            _runners.Clear();
            RunnersCount = 0;
        }

        public string GetStats()
        {
            StringBuilder builder       = new StringBuilder();
            StringBuilder failedBuilder = new StringBuilder();
            int           index         = 1;
            for (var i = 0; i < _runners.Count; i++)
            {
                if (_runners[i] is RandomlyFailingRunner { HasFailed: true })
                {
                    continue;
                }

                builder.AppendLine($"{index++} - {_runners[i].GetStats()}");
            }

            for (var i = 0; i < _runners.Count; i++)
            {
                if (_runners[i] is RandomlyFailingRunner { HasFailed: true })
                {
                    failedBuilder.AppendLine($"{index++} - {_runners[i].GetStats()}");
                }
            }

            return builder.ToString() + failedBuilder.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunnerSim
{
    public partial class Form1 : Form
    {
        // количество бегунов
        private int _runnersCount = 4;

        // количество дорожек
        private int _trackCount = 8;

        // высота одной дорожки
        private int _trackHeight = 30;

        // отступ между дорожками
        private int _trackMargin = 10;

        // отступ дорожки слева
        private int _tracksLeftOffset = 30;

        // длина стадиона
        private int _stadiumLength = 30;

        // бегуны и судья
        private List<Runner> _runners = new();
        private Referee _referee = new();


        // сообщение судьи
        private string _refereeMessage = "";
        private bool _isResetRequired;

        // цвет дорожки
        private Brush _trackBackgroundBrush = new SolidBrush(Color.FromArgb(0xff, 242, 68, 4));

        // рандом
        private Random _random = new(DateTime.Now.Millisecond);

        // картинки
        private Image _refereeImage = Image.FromFile("Resources/Referee.png");
        private Image _grassImage = Image.FromFile("Resources/grass.jpg");

        private Image[] _runnerImages = new[]
        {
            Image.FromFile("Resources/runner0.png"),
            Image.FromFile("Resources/runner1.png"),
            Image.FromFile("Resources/runner2.png"),
            Image.FromFile("Resources/runner3.png"),
            Image.FromFile("Resources/runner4.png"),
            Image.FromFile("Resources/runner5.png"),
            Image.FromFile("Resources/runner6.png"),
            Image.FromFile("Resources/runner7.png"),
            Image.FromFile("Resources/runner8.png"),
            Image.FromFile("Resources/runner9.png"),
        };

        public Form1()
        {
            InitializeComponent();
        }

        // Открытие приложения
        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDownRunners.Value       = _runnersCount;
            numericUpDownTracks.Value        = _trackCount;
            numericUpDownStadiumLength.Value = _stadiumLength;
            for (var i = 0; i < _runnersCount; i++)
            {
                AddRunner();
            }

            _referee.RaceFinished += OnRaceFinished;
        }

        // изменение количества бегунов
        private void numericUpDownRunners_ValueChanged(object sender, EventArgs e)
        {
            var newValue = (int)numericUpDownRunners.Value;

            if (newValue > _trackCount)
            {
                MessageBox.Show("Невозможно добавить бегуна т.к. нет свободной дорожки");
                numericUpDownRunners.Value = _runnersCount;
                return;
            }

            if (newValue > _runnersCount)
            {
                for (int i = 0; i < newValue - _runnersCount; i++)
                {
                    AddRunner();
                }
            }
            else
            {
                for (int i = 0; i < _runnersCount - newValue; i++)
                {
                    var runner = _runners[_runners.Count - 1];
                    _referee.RaceStarted -= runner.OnRaceStart;
                    _runners.RemoveAt(_runners.Count - 1);
                }
            }

            _runnersCount = newValue;
            pictureBoxCanvas.Refresh();
        }

        // добавление бегуна
        private void AddRunner()
        {
            Runner runner;
            if (_runners.Count < 5)
            {
                runner = new Runner(_random.Next(7, 13));
            }
            else
            {
                runner = new BarrierRunner(_random.Next(7, 13));
            }

            _referee.RaceStarted += runner.OnRaceStart;
            _runners.Add(runner);
        }

        // изменение количества дорожек
        private void numericUpDownTracks_ValueChanged(object sender, EventArgs e)
        {
            var newValue = (int)numericUpDownTracks.Value;
            if (newValue < _runnersCount)
            {
                MessageBox.Show("Невозможно уменьшить кол-во дорожек, т.к. на них есть бегуны");
                numericUpDownTracks.Value = _trackCount;
                return;
            }

            _trackCount = newValue;
            pictureBoxCanvas.Refresh();
        }

        // изменение длины стадиона
        private void numericUpDownStadiumLength_ValueChanged(object sender, EventArgs e)
        {
            var newValue = (int)numericUpDownStadiumLength.Value;
            _stadiumLength = newValue;
        }

        // отрисовка
        private void pictureBoxCanvas_Paint(object sender, PaintEventArgs e)
        {
            int stadiumOffsetY = pictureBoxCanvas.Height - (_trackHeight + _trackMargin) * 10;
            e.Graphics.DrawImage(_grassImage, 0, stadiumOffsetY, pictureBoxCanvas.Width, pictureBoxCanvas.Height - stadiumOffsetY);
            for (var i = 0; i < _trackCount; i++)
            {
                // фон дорожки
                e.Graphics.FillRectangle(_trackBackgroundBrush, 0, stadiumOffsetY + i * (_trackHeight + _trackMargin), pictureBoxCanvas.Width, _trackHeight);

                // координаты начала и конца трека
                float trackStart = _tracksLeftOffset + i * 20;
                float trackEnd   = pictureBoxCanvas.Width - (_trackCount - i + 3) * 20;

                // стартовая черта
                e.Graphics.FillRectangle(Brushes.White, trackStart, stadiumOffsetY + i * (_trackHeight + _trackMargin), 5, _trackHeight);

                // финишная черта
                e.Graphics.FillRectangle(Brushes.White, trackEnd, stadiumOffsetY + i * (_trackHeight + _trackMargin), 5, _trackHeight);

                if (i < _runnersCount)
                {
                    // бегун

                    _runners[i].Draw(e.Graphics, stadiumOffsetY, i, trackStart, trackEnd, _trackHeight, _trackMargin, _runnerImages);
                }
            }

            e.Graphics.DrawImage(_refereeImage, 0, 0, 30, 30);

            e.Graphics.FillRectangle(Brushes.Yellow, 30, 0, pictureBoxCanvas.Width - 30 - 1, stadiumOffsetY - 1);
            e.Graphics.DrawRectangle(Pens.Black, 30, 0, pictureBoxCanvas.Width - 30 - 1, stadiumOffsetY - 1);

            var refereeMessageSize = e.Graphics.MeasureString(_refereeMessage, DefaultFont);

            e.Graphics.DrawString(_refereeMessage, DefaultFont, Brushes.Black, 30 + (pictureBoxCanvas.Width - 30) / 2 - refereeMessageSize.Width / 2, stadiumOffsetY / 2 - refereeMessageSize.Height / 2);
        }

        // таймер перерисовки
        private void timerRace_Tick(object sender, EventArgs e)
        {
            float deltaSeconds = timerRace.Interval / 1000f;
            for (var i = 0; i < _runnersCount; i++)
            {
                if (!_runners[i].HasStopped)
                {
                    _runners[i].OnRaceTick(_stadiumLength, deltaSeconds);
                }
            }

            pictureBoxCanvas.Refresh();
        }

        // нажатие на кнопку старт/сброс
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (_isResetRequired)
            {
                for (var i = 0; i < _runners.Count; i++)
                {
                    _runners[i].Reset();
                    _runners[i].Speed = _random.Next(7, 13);
                }

                _referee.Reset();

                _refereeMessage  = "";
                buttonStart.Text = "Старт";
                pictureBoxCanvas.Refresh();
                _isResetRequired = false;
                return;
            }

            numericUpDownRunners.Enabled       = false;
            numericUpDownTracks.Enabled        = false;
            numericUpDownStadiumLength.Enabled = false;
            buttonStart.Enabled                = false;

            _referee.RunnersCount = _runnersCount;

            _refereeMessage = "Старт";
            pictureBoxCanvas.Refresh();
            await Task.Delay(1000);
            _refereeMessage = "Внимание";
            pictureBoxCanvas.Refresh();
            await Task.Delay(1000);
            _refereeMessage = "Марш";
            pictureBoxCanvas.Refresh();

            for (var i = 0; i < _runners.Count; i++)
            {
                _runners[i].Index = i + 1;
            }

            _referee.StartRace();

            timerRace.Start();
        }

        // обработка события завершения гонки
        private async void OnRaceFinished()
        {
            _refereeMessage = "Подсчёт результатов";
            pictureBoxCanvas.Refresh();
            await Task.Delay(2000);

            timerRace.Stop();

            _refereeMessage = _referee.GetStats();
            pictureBoxCanvas.Refresh();

            numericUpDownRunners.Enabled       = true;
            numericUpDownTracks.Enabled        = true;
            numericUpDownStadiumLength.Enabled = true;
            buttonStart.Enabled                = true;
            buttonStart.Text                   = "Сброс";
            _isResetRequired                   = true;
        }
    }
}
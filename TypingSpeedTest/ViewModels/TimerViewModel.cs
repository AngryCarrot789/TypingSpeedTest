using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TypingSpeedTest.Utilities;

namespace TypingSpeedTest.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        private double _wordTimerSpeed;
        public double WordTimerSpeed
        {
            get => _wordTimerSpeed;
            set => RaisePropertyChanged(ref _wordTimerSpeed, value, SetNewWordTimerInterval);
        }

        public WordsPresenterViewModel WordsPresenter { get; set; }

        public DispatcherTimer NewWordTimer { get; set; }

        public TimerViewModel(WordsPresenterViewModel wordsPresenter)
        {
            WordsPresenter = wordsPresenter;
            WordTimerSpeed = 2;
            Reset();
        }

        public void Reset()
        {
            SetupTimer();
            WordsPresenter.ClearWordsList();
            WordsPresenter.ClearInputWord();
        }

        public void SetupTimer()
        {
            NewWordTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(WordTimerSpeed)
            };
            NewWordTimer.Tick += NewWordTimer_Tick;
            NewWordTimer.Start();
        }
        public void SetNewWordTimerInterval(double value)
        {
            if (NewWordTimer != null)
                NewWordTimer.Interval = TimeSpan.FromSeconds(value);
        }

        private void NewWordTimer_Tick(object sender, EventArgs e)
        {
            WordsPresenter.AddNewRandomWord();
        }
    }
}

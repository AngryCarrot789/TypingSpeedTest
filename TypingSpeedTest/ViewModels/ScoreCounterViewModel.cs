using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingSpeedTest.Utilities;

namespace TypingSpeedTest.ViewModels
{
    public class ScoreCounterViewModel : BaseViewModel
    {
        private int _correctScore;
        private int _incorrectScore;

        public int CorrectScore
        {
            get => _correctScore;
            set => RaisePropertyChanged(ref _correctScore, value);
        }

        public int IncorrectScore
        {
            get => _incorrectScore;
            set => RaisePropertyChanged(ref _incorrectScore, value);
        }

        public void ResetCorrectScore() => CorrectScore = 0;
        public void ResetIncorrectScore() => IncorrectScore = 0;
        public void IncreaseCorrectScore() => CorrectScore++;
        public void IncreaseIncorrectScore() => IncorrectScore++;
        public void DecreaseCorrectScore() => CorrectScore--;
        public void DecreaseIncorrectScore() => IncorrectScore--;
    }
}

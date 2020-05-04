using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TypingSpeedTest.Utilities;
using TypingSpeedTest.Words;

namespace TypingSpeedTest.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public WordsPresenterViewModel WordsPresenter { get; set; }
        public TimerViewModel WordTimer { get; set; }
        public MainViewModel()
        {
            WordsPresenter = new WordsPresenterViewModel();
            WordTimer = new TimerViewModel(WordsPresenter);
        }
    }
}

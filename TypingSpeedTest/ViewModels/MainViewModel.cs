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
        #region Private Fields

        private Brush _foregroundColour = new SolidColorBrush(Colors.White);
        private string _inputWord;
        private string _selectedWord;
        private int _selectedWordIndex;
        private double _wordTimerSpeed;

        #endregion

        #region Public Fields

        public Brush InputWordForeground
        {
            get => _foregroundColour;
            set => RaisePropertyChanged(ref _foregroundColour, value);
        }
        public string InputWord
        {
            get => _inputWord;
            set => RaisePropertyChanged(ref _inputWord, value, InputWordTextChanged);
        }
        public string SelectedWord
        {
            get => _selectedWord;
            set => RaisePropertyChanged(ref _selectedWord, value);
        }
        public int SelectedWordIndex
        {
            get => _selectedWordIndex;
            set => RaisePropertyChanged(ref _selectedWordIndex, value);
        }
        public double WordTimerSpeed
        {
            get => _wordTimerSpeed;
            set => RaisePropertyChanged(ref _wordTimerSpeed, value, SetNewWordTimerInterval);
        }

        #endregion

        public ICommand CheckWordCommand { get; set; }
        public ICommand MoveWordIndexCommand { get; set; }
        public ICommand ClearWordsListCommand { get; set; }
        public ICommand ClearScoreCommand { get; set; }
        public ICommand SkipWordCommand { get; set; }
        public ICommand ResetIndexCommand { get; set; }

        public ScoreCounterViewModel ScoreCounter { get; set; }
        public ObservableCollection<string> WordsList { get; set; }
        public DispatcherTimer NewWordTimer { get; set; }

        public MainViewModel()
        {
            ScoreCounter = new ScoreCounterViewModel();
            WordsList = new ObservableCollection<string>();
            CheckWordCommand = new Command(CheckWord);
            MoveWordIndexCommand = new CommandParam(MoveWordIndex);
            ClearWordsListCommand = new Command(ClearWordsList);
            SkipWordCommand = new Command(SkipWord);
            ResetIndexCommand = new Command(SetIndexAtStart);
            WordTimerSpeed = 2;
            Reset();
        }

        public void Reset()
        {
            SetupTimer();
            ClearWordsList();
            ClearInputWord();
        }

        // when you just cant be bothered
        public void SkipWord()
        {
            if (SelectedWord.IsNotEmpty())
            {
                InputWord = SelectedWord;
                CheckWord();
            }
        }

        #region Timer

        public void SetupTimer()
        {
            NewWordTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(WordTimerSpeed)
            };
            NewWordTimer.Tick += NewWordTimer_Tick;
            NewWordTimer.Start();
        }

        private void NewWordTimer_Tick(object sender, EventArgs e)
        {
            AddNewRandomWord();
        }

        private void SetNewWordTimerInterval(double value)
        {
            if (NewWordTimer != null)
                NewWordTimer.Interval = TimeSpan.FromSeconds(value);
        }

        #endregion

        #region Modifying WordsList and InputWord

        public void AddNewRandomWord() => WordsList.Add(RandomWordGenerator.RandomWord());
        public void ClearWordsList() => WordsList.Clear();
        public void ClearScore()
        {
            ScoreCounter.ResetCorrectScore();
            ScoreCounter.ResetIncorrectScore();
        }
        public void ClearInputWord() => InputWord = string.Empty;

        #endregion

        #region Dealing with word detection stuff

        public void InputWordTextChanged()
        {
            CheckWordIsCorrect();
        }

        public void CheckWordIsCorrect()
        {
            if (SelectedWord.IsNotNull() && InputWord.IsNotNull() && !SelectedWord.Contains(InputWord))
                InputWordForeground = new SolidColorBrush(Colors.Red);
            else
                InputWordForeground = new SolidColorBrush(Colors.White);
        }

        public void CheckWord()
        {
            if (SelectedWord.IsNotNull() && InputWord.IsNotNull() &&
                InputWord.ToLower() == SelectedWord)
            {
                CorrectWordEntered();
            }
            else
            {
                IncorrectWordEntered();
            }
        }

        public void CorrectWordEntered()
        {
            if (SelectedWord.IsNotNull())
            {
                WordsList.Remove(SelectedWord);
                ClearInputWord();
                SetIndexAtStart();
                ScoreCounter.IncreaseCorrectScore();
            }
        }

        public void IncorrectWordEntered()
        {
            ClearInputWord();
            ScoreCounter.IncreaseIncorrectScore();
        }

        #endregion

        #region ListBox modification things; selectionindexes

        public void MoveWordIndex(object upOrDown)
        {
            int direction = int.Parse(upOrDown.ToString());
            if (direction == 1 && SelectedWordIndex > 0)
            {
                SelectedWordIndex--;
                CheckWordIsCorrect();
            }
            else if (direction == 0 && SelectedWordIndex < WordsList.Count)
            {
                SelectedWordIndex++;
                CheckWordIsCorrect();
            }
        }
        
        public void SetIndexAtStart()
        {
            if (WordsList != null && WordsList.Count > 0)
                //SelectedIndex = WordsList.Count - 1;
                SelectedWordIndex = 0;
        }

        #endregion
    }
}

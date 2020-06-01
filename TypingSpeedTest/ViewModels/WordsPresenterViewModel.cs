using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TypingSpeedTest.Utilities;
using TypingSpeedTest.Words;
// shut it. i dont actually care
using Colour = System.String;
namespace TypingSpeedTest.ViewModels
{
    public class WordsPresenterViewModel : BaseViewModel
    {
        #region Private Fields

        private Colour _foregroundColour;
        private string _inputWord;
        private string _selectedWord;
        private int _selectedWordIndex;

        #endregion

        #region Public Fields

        public Colour InputWordForeground
        {
            get => _foregroundColour;
            set => RaisePropertyChanged(ref _foregroundColour, value);
        }
        public string InputWord
        {
            get => _inputWord;
            set => RaisePropertyChanged(ref _inputWord, value, ()=>
            {
                value = value.Trim();
                InputWordTextChanged();
            });
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

        public ICommand CheckWordCommand { get; set; }
        public ICommand MoveWordIndexCommand { get; set; }
        public ICommand ClearWordsListCommand { get; set; }
        public ICommand ClearScoreCommand { get; set; }
        public ICommand SkipWordCommand { get; set; }
        public ICommand ResetIndexCommand { get; set; }

        #endregion

        public ObservableCollection<string> WordsList { get; set; }
        public ScoreCounterViewModel ScoreCounter { get; set; }

        public WordsPresenterViewModel()
        {
            ScoreCounter          = new ScoreCounterViewModel();
            WordsList             = new ObservableCollection<string>();
            CheckWordCommand      = new Command(CheckWord);
            ClearWordsListCommand = new Command(ClearWordsList);
            SkipWordCommand       = new Command(SkipWord);
            ClearScoreCommand     = new Command(ClearScore);
            ResetIndexCommand     = new Command(SetIndexAtStart);
            MoveWordIndexCommand  = new CommandParam<int>(MoveWordIndex);
        }

        // when you just cant be bothered
        public void SkipWord()
        {
            if (SelectedWord.IsNotEmpty())
            {
                InputWord = SelectedWord;
                CheckWord();
                ScoreCounter.DecreaseCorrectScore();
                ScoreCounter.IncreaseIncorrectScore();
            }
        }

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
                InputWordForeground = Colors.Red.ToString();
            else
                InputWordForeground = "#FFEBEBEB";
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

        public void MoveWordIndex(int direction)
        {
            bool up = direction == 1;
            if (up && SelectedWordIndex > 0)
            {
                SelectedWordIndex--;
                CheckWordIsCorrect();
            }
            else if (!up && SelectedWordIndex < WordsList.Count)
            {
                SelectedWordIndex++;
                CheckWordIsCorrect();
            }
            if (SelectedWordIndex == -1 && WordsList.Count > 0)
                SelectedWordIndex = 0;
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

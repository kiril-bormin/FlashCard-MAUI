using FlashCard.Models;
using FlashCard.Services;

namespace FlashCard
{
    [QueryProperty(nameof(CorrectCount), "correct")]
    [QueryProperty(nameof(TotalCount), "total")]
    [QueryProperty(nameof(DeckName), "deckName")]
    [QueryProperty(nameof(TimeSpent), "timeSpent")]
    [QueryProperty(nameof(MostDifficultCardFront), "mostDifficultCardFront")]
    [QueryProperty(nameof(PerfectCardsCount), "perfectCardsCount")]
    [QueryProperty(nameof(OriginalTotalCount), "originalTotalCount")]
    [QueryProperty(nameof(CurrentDeck), "deck")]
    [QueryProperty(nameof(Decks), "decks")]
    [QueryProperty(nameof(DataService), "dataService")]
    public partial class LearnResultPage : ContentPage
    {
        private Deck _currentDeck;
        public Deck CurrentDeck
        {
            get => _currentDeck;
            set => _currentDeck = value;
        }

        private List<Deck> _decks;
        public List<Deck> Decks
        {
            get => _decks;
            set => _decks = value;
        }

        private JsonDataService _dataService;
        public JsonDataService DataService
        {
            get => _dataService;
            set => _dataService = value;
        }

        private int _correct;
        public int CorrectCount
        {
            get => _correct;
            set
            {
                _correct = value;
                UpdateUI();
            }
        }

        private int _total;
        public int TotalCount
        {
            get => _total;
            set
            {
                _total = value;
                UpdateUI();
            }
        }

        private string _deckName;
        public string DeckName
        {
            get => _deckName;
            set
            {
                _deckName = value;
                DeckNameLabel.Text = _deckName;
            }
        }

        private double _timeSpent;
        public double TimeSpent
        {
            get => _timeSpent;
            set
            {
                _timeSpent = value;
                UpdateUI();
            }
        }

        private string _mostDifficultCardFront;
        public string MostDifficultCardFront
        {
            get => _mostDifficultCardFront;
            set
            {
                _mostDifficultCardFront = value;
                UpdateUI();
            }
        }

        private int _perfectCardsCount;
        public int PerfectCardsCount
        {
            get => _perfectCardsCount;
            set
            {
                _perfectCardsCount = value;
                UpdateUI();
            }
        }

        private int _originalTotalCount;
        public int OriginalTotalCount
        {
            get => _originalTotalCount;
            set
            {
                _originalTotalCount = value;
                UpdateUI();
            }
        }

        public LearnResultPage()
        {
            InitializeComponent();
        }

        private void UpdateUI()
        {
            if (_total == 0 || _originalTotalCount == 0) return;

            double memorizationPercentage = (double)_perfectCardsCount / _originalTotalCount * 100;
            PercentageLabel.Text = $"{memorizationPercentage:F0}% de mémorisation";

            if (memorizationPercentage >= 80)
                PercentageLabel.TextColor = Color.FromArgb("#4CAF50");
            else if (memorizationPercentage >= 50)
                PercentageLabel.TextColor = Color.FromArgb("#FFC107");
            else
                PercentageLabel.TextColor = Color.FromArgb("#F44336");

            TimeSpan ts = TimeSpan.FromSeconds(_timeSpent);
            TimeSpentLabel.Text = $"{(int)ts.TotalMinutes} min {ts.Seconds} s";

            PerfectCardsLabel.Text = $"{_perfectCardsCount} / {_originalTotalCount}";
            DifficultCardLabel.Text = string.IsNullOrEmpty(_mostDifficultCardFront) ? "Aucune" : _mostDifficultCardFront;
        }

        private async void OnRestartClicked(object sender, EventArgs e)
        {
            if (_currentDeck == null) return;
            
            var navigationParameter = new Dictionary<string, object>
            {
                { "deck", _currentDeck },
                { "decks", _decks },
                { "dataService", _dataService }
            };
            
            await Shell.Current.GoToAsync($"../{nameof(LearnPage)}", navigationParameter);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
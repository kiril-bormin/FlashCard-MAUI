namespace FlashCard
{
    [QueryProperty(nameof(CorrectCount), "correct")]
    [QueryProperty(nameof(TotalCount), "total")]
    [QueryProperty(nameof(DeckName), "deckName")]
    public partial class LearnResultPage : ContentPage
    {
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

        public LearnResultPage()
        {
            InitializeComponent();
        }

        private void UpdateUI()
        {
            if (_total == 0) return;

            ScoreLabel.Text = $"{_correct} / {_total}";
            double percentage = (double)_correct / _total * 100;
            PercentageLabel.Text = $"{percentage:F0}%";

            if (percentage >= 80)
                PercentageLabel.TextColor = Color.FromArgb("#4CAF50");
            else if (percentage >= 50)
                PercentageLabel.TextColor = Color.FromArgb("#FFC107");
            else
                PercentageLabel.TextColor = Color.FromArgb("#F44336");
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
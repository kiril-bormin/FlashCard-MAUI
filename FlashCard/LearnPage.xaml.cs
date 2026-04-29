using FlashCard.Models;

namespace FlashCard
{
    [QueryProperty(nameof(CurrentDeck), "deck")]
    public partial class LearnPage : ContentPage
    {
        private Deck _deck;
        public Deck CurrentDeck
        {
            get => _deck;
            set
            {
                _deck = value;
                StartSession();
            }
        }

        private List<Card> _shuffledCards;
        private int _currentIndex = 0;
        private bool _isShowingBack = false;
        private int _correctCount = 0;

        public LearnPage()
        {
            InitializeComponent();
        }

        private void StartSession()
        {
            if (_deck == null || _deck.Cards.Count == 0) return;

            DeckNameLabel.Text = _deck.Name;
            _shuffledCards = _deck.Cards.OrderBy(x => Guid.NewGuid()).ToList();
            _currentIndex = 0;
            _correctCount = 0;
            
            ShowCard();
        }

        private void ShowCard()
        {
            _isShowingBack = false;
            var card = _shuffledCards[_currentIndex];
            CardContentLabel.Text = card.Front;
            SideIndicatorLabel.Text = "(Appuyez pour voir le verso)";
            ProgressLabel.Text = $"Carte {_currentIndex + 1} / {_shuffledCards.Count}";
            
            ActionButtons.IsVisible = false;
            InstructionLabel.IsVisible = true;
        }

        private void OnCardTapped(object sender, EventArgs e)
        {
            _isShowingBack = !_isShowingBack;
            var card = _shuffledCards[_currentIndex];
            
            if (_isShowingBack)
            {
                CardContentLabel.Text = card.Back;
                SideIndicatorLabel.Text = "(Appuyez pour voir le recto)";
                ActionButtons.IsVisible = true;
                InstructionLabel.IsVisible = false;
            }
            else
            {
                CardContentLabel.Text = card.Front;
                SideIndicatorLabel.Text = "(Appuyez pour voir le verso)";
            }
        }

        private async void OnCorrectClicked(object sender, EventArgs e)
        {
            _correctCount++;
            await NextCard();
        }

        private async void OnWrongClicked(object sender, EventArgs e)
        {
            await NextCard();
        }

        private async Task NextCard()
        {
            _currentIndex++;
            if (_currentIndex < _shuffledCards.Count)
            {
                ShowCard();
            }
            else
            {
                // Session finished
                var navigationParameter = new Dictionary<string, object>
                {
                    { "correct", _correctCount },
                    { "total", _shuffledCards.Count },
                    { "deckName", _deck.Name }
                };
                await Shell.Current.GoToAsync("LearnResultPage", navigationParameter);
            }
        }
    }
}
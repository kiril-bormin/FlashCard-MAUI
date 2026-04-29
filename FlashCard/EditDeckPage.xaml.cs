using FlashCard.Models;
using FlashCard.Services;

namespace FlashCard
{
    public partial class EditDeckPage : ContentPage, IQueryAttributable
    {
        private Deck _deck;
        private int _cardCount;
        private JsonDataService _dataService;
        private List<Deck> _decks;
        private bool _isInitializing;

        public EditDeckPage()
        {
            InitializeComponent();
        }

        // Receive navigation parameters
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("deck", out object? deckObj) && deckObj is Deck deck)
            {
                _deck = deck;
                _cardCount = deck.CardCount;

                _isInitializing = true;
                // Initialize fields
                NameEntry.Text = deck.Name;
                _isInitializing = false;

                RefreshCards();
            }

            if (query.TryGetValue("dataService", out object? serviceObj) && serviceObj is JsonDataService service)
            {
                _dataService = service;
            }

            if (query.TryGetValue("decks", out object? decksObj) && decksObj is List<Deck> decks)
            {
                _decks = decks;
            }
        }

        private void RefreshCards()
        {
            CardsCollectionView.ItemsSource = null;
            CardsCollectionView.ItemsSource = _deck?.Cards;
        }

        private async void OnAddCardClicked(object sender, EventArgs e)
        {
            if (_deck == null) return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "deck", _deck },
                { "dataService", _dataService },
                { "decks", _decks }
            };

            await Shell.Current.GoToAsync(nameof(AddCardPage), navigationParameter);
        }

        private async void OnNameTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isInitializing && _deck != null && _dataService != null)
            {
                _deck.Name = e.NewTextValue;
                await _dataService.SaveDecksAsync(_decks);
            }
        }
    }
}
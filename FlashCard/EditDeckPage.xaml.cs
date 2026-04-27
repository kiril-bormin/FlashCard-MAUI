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

                // Initialize fields
                NameEntry.Text = deck.Name;
                CardCountLabel.Text = _cardCount.ToString();
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

        private void OnIncrementClicked(object sender, EventArgs e)
        {
            _cardCount++;
            CardCountLabel.Text = _cardCount.ToString();
        }

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            if (_cardCount > 0)
            {
                _cardCount--;
                CardCountLabel.Text = _cardCount.ToString();
            }
        }
        private async void OnAddCardClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FrontEntry.Text) || string.IsNullOrWhiteSpace(BackEntry.Text))
            {
                await DisplayAlert("Erreur", "Veuillez remplir le recto et le verso", "OK");
                return;
            }

            if (_deck.Cards == null) _deck.Cards = new List<Card>();

            _deck.Cards.Add(new Card
            {
                Front = FrontEntry.Text.Trim(),
                Back = BackEntry.Text.Trim()
            });

            _cardCount = _deck.Cards.Count;
            CardCountLabel.Text = _cardCount.ToString();
            _deck.CardCount = _cardCount;

            await _dataService.SaveDecksAsync(_decks);

            FrontEntry.Text = string.Empty;
            BackEntry.Text = string.Empty;

            RefreshCards();

            await DisplayAlert("Succès", "Carte ajoutée au deck !", "OK");
        }
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string? newName = NameEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(newName))
            {
                await DisplayAlert("Erreur", "Le nom ne peut pas �tre vide", "OK");
                return;
            }

            // Update deck
            _deck.Name = newName;
            _deck.CardCount = _cardCount;

            // Save immediately to JSON
            await _dataService.SaveDecksAsync(_decks);

            await Shell.Current.GoToAsync("..");
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
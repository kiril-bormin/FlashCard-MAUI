using FlashCard.Models;
using FlashCard.Services;

namespace FlashCard
{
    public partial class DecksPage : ContentPage
    {
        private JsonDataService _dataService;
        private List<Deck> _decks;
        private int _nextId = 1;

        public DecksPage()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            _decks = new List<Deck>();
            LoadDecks();
        }

        private async void LoadDecks()
        {
            _decks = await _dataService.LoadDecksAsync();
            if (_decks.Any())
            {
                _nextId = _decks.Max(d => d.Id) + 1;
            }
            RefreshView();
            UpdateInfo($"Charg�: {_decks.Count} deck(s)");
        }

        private void RefreshView()
        {
            DecksCollectionView.ItemsSource = null;
            DecksCollectionView.ItemsSource = _decks;
        }

        private void UpdateInfo(string message)
        {
            InfoLabel.Text = $"{DateTime.Now:HH:mm:ss} - {message}";
        }

        // --- NOUVELLES M�THODES � AJOUTER ---

        private async void OnAddDeckClicked(object sender, EventArgs e)
        {
            string name = NewDeckEntry.Text?.Trim();
            if (string.IsNullOrEmpty(name)) return;

            var newDeck = new Deck { Id = _nextId++, Name = name, CardCount = 0, CreatedDate = DateTime.Now };
            _decks.Add(newDeck);
            await _dataService.SaveDecksAsync(_decks);

            NewDeckEntry.Text = string.Empty;
            RefreshView();
            UpdateInfo($"Ajout�: {name}");
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await _dataService.SaveDecksAsync(_decks);
            RefreshView(); // Force le rafra�chissement pour repasser en mode lecture
            UpdateInfo("Modifications enregistr�es");
        }

        private async void OnDeleteDeckClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            Deck? deck = button?.CommandParameter as Deck;

            if (deck == null) return;

            // Confirm deletion
            bool confirm = await DisplayAlert(
                "Confirmation",
                $"Voulez-vous vraiment supprimer '{deck.Name}' ?",
                "Supprimer",
                "Annuler"
            );

            if (!confirm) return;

            // Remove deck
            _decks.Remove(deck);
            await _dataService.SaveDecksAsync(_decks);

            RefreshView();
            UpdateInfo($"Supprim�: {deck.Name}");
        }

        // --- M�THODES D'AFFICHAGE D�J� PR�SENTES ---

        private void OnEditDeckInlineClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var grid = (Grid)button.Parent;
            grid.FindByName<StackLayout>("DisplayStack").IsVisible = false;
            grid.FindByName<HorizontalStackLayout>("EditStack").IsVisible = true;
            button.IsVisible = false;
        }

        private void OnCancelEditClicked(object sender, EventArgs e)
        {
            RefreshView();
        }

        private async void OnDeckTapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var deck = (Deck)frame.BindingContext;

            if (deck == null) return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "deck", deck },
                { "dataService", _dataService },
                { "decks", _decks }
            };

            await Shell.Current.GoToAsync(nameof(EditDeckPage), navigationParameter);
        }
    

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshView();
        }
    }
}
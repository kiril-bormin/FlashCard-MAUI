using FlashCard.Models;
using FlashCard.Services;

namespace FlashCard
{
    public partial class LearnSelectionPage : ContentPage
    {
        private JsonDataService _dataService;

        public LearnSelectionPage()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var decks = await _dataService.LoadDecksAsync();
            DecksCollectionView.ItemsSource = decks;
        }

        private async void OnDeckSelected(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var deck = (Deck)frame.BindingContext;

            if (deck == null || deck.Cards.Count == 0)
            {
                await DisplayAlert("Deck vide", "Ce deck ne contient pas de cartes.", "OK");
                return;
            }

            var navigationParameter = new Dictionary<string, object>
            {
                { "deck", deck }
            };

            await Shell.Current.GoToAsync("LearnPage", navigationParameter);
        }
    }
}
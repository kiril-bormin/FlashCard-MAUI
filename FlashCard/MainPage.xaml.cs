using FlashCard.Models;
using FlashCard.Services;

namespace FlashCard
{
    public partial class MainPage : ContentPage
    {
        private JsonDataService _dataService = new JsonDataService();
        public List<Deck> MyDecks { get; set; }
        public MainPage()
        {
            InitializeComponent();
            MyDecks = DeckSeeder.GetDefaultDecks();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            var decks = await _dataService.LoadDecksAsync();
            int toLearn = 0;
            int mastered = 0;

            foreach (var deck in decks)
            {
                if (deck.Cards != null)
                {
                    foreach (var card in deck.Cards)
                    {
                        if (card.IsMastered) mastered++;
                        else toLearn++;
                    }
                }
            }

            ToLearnLabel.Text = toLearn.ToString();
            MasteredLabel.Text = mastered.ToString();
        }

        private async void AddCardButton(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddCardPage");
        }
        private async void OnDecksClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("DecksPage");
        }

        private async void OnLearnClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("LearnSelectionPage");
        }
    }
}

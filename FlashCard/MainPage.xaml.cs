namespace FlashCard
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void AddCardButton(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddCardPage");
        }
        private async void OnDecksClicked(object sender, EventArgs e)
        {
            // Navigation vers la page "Mes Decks"
            await Shell.Current.GoToAsync("DecksPage");
        }

        private async void OnLearnClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("LearnSelectionPage");
        }
    }

}

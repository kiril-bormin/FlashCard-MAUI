namespace FlashCard
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute("AddCardPage", typeof(FlashCard.AddCardPage));
        }

        private async void AddCardButton(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddCardPage");
        }
    }

}

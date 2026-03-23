namespace FlashCard
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            // Accès au bouton via sender
            Button button = (Button)sender;
            button.Text = "Cliqué !";

            // Ou via x:Name
            btnClickMe.BackgroundColor = Colors.Green;
        }
    }

}

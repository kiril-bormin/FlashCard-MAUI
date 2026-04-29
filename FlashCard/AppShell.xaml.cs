namespace FlashCard
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EditDeckPage), typeof(EditDeckPage));
            Routing.RegisterRoute(nameof(AddCardPage), typeof(AddCardPage));
            Routing.RegisterRoute("DecksPage", typeof(DecksPage));
            Routing.RegisterRoute("LearnSelectionPage", typeof(LearnSelectionPage));
            Routing.RegisterRoute("LearnPage", typeof(LearnPage));
            Routing.RegisterRoute("LearnResultPage", typeof(LearnResultPage));
        }
    }
}

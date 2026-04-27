namespace FlashCard
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EditDeckPage), typeof(EditDeckPage));
        }
    }
}

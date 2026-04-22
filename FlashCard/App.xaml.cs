namespace FlashCard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent(); // cette méthode fusionne le c# et xaml 

            MainPage = new AppShell(); // AppShell gère la hiérarchie des pages 
        }
    }
}

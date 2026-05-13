using FlashCard.Models;
using Microsoft.Maui.Devices.Sensors;
using Plugin.Maui.Audio;

namespace FlashCard
{
    [QueryProperty(nameof(CurrentDeck), "deck")]
    public partial class LearnPage : ContentPage
    {
        private Deck _deck;
        public Deck CurrentDeck
        {
            get => _deck;
            set
            {
                _deck = value;
                StartSession();
            }
        }

        private List<Card> _shuffledCards;
        private int _currentIndex = 0;
        private bool _isShowingBack = false;
        private int _correctCount = 0;
        private DateTime _lastShakeTime = DateTime.MinValue;

        public LearnPage()
        {
            InitializeComponent();

            // Vérifier si l'accéléromètre est disponible
            if (Accelerometer.Default.IsSupported)
            {
                Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;

                // Démarrer la surveillance
                if (!Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.Start(SensorSpeed.UI);
                }
            }
        }

        private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            // Cooldown de 2 secondes pour éviter les déclenchements multiples
            if ((DateTime.Now - _lastShakeTime).TotalSeconds < 2)
                return;
            _lastShakeTime = DateTime.Now;

            // Jouer le son de skip en arrière-plan
            try
            {
                var audioPlayer = AudioManager.Current.CreatePlayer(
                    await FileSystem.OpenAppPackageFileAsync("skip.mp3")
                );
                audioPlayer.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur audio : {ex.Message}");
            }

            // Skip la carte et la marquer comme fausse sur le thread principal
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (_shuffledCards != null && _currentIndex < _shuffledCards.Count)
                {
                    OnWrongClicked(sender, e);
                }
            });
        }

        protected override void OnDisappearing()
        {
            if (Accelerometer.Default.IsSupported)
            {
                // Arrêter la surveillance et supprimer les gestionnaires d'événements
                Accelerometer.Default.Stop();
                Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
            }
            base.OnDisappearing();
        }

        private void StartSession()
        {
            if (_deck == null || _deck.Cards.Count == 0) return;

            DeckNameLabel.Text = _deck.Name;
            _shuffledCards = _deck.Cards.OrderBy(x => Guid.NewGuid()).ToList();
            _currentIndex = 0;
            _correctCount = 0;
            
            ShowCard();
        }

        private void ShowCard()
        {
            _isShowingBack = false;
            var card = _shuffledCards[_currentIndex];
            CardContentLabel.Text = card.Front;
            SideIndicatorLabel.Text = "(Appuyez pour voir le verso)";
            ProgressLabel.Text = $"Carte {_currentIndex + 1} / {_shuffledCards.Count}";
            
            ActionButtons.IsVisible = false;
            InstructionLabel.IsVisible = true;
        }

        private async void OnCardTapped(object sender, EventArgs e)
        {
            await CardFrame.RotateYTo(90, 250, Easing.CubicIn);


            _isShowingBack = !_isShowingBack;
            var card = _shuffledCards[_currentIndex];

            if (_isShowingBack)
            {
                CardContentLabel.Text = card.Back;
                SideIndicatorLabel.Text = "(Appuyez pour voir le recto)";
                ActionButtons.IsVisible = true;
                InstructionLabel.IsVisible = false;
            }
            else
            {
                CardContentLabel.Text = card.Front;
                SideIndicatorLabel.Text = "(Appuyez pour voir le verso)";
            }

            CardFrame.RotationY = -90;

            await CardFrame.RotateYTo(0, 250, Easing.CubicOut);
        }

        private async void OnCorrectClicked(object sender, EventArgs e)
        {
            _correctCount++;
            await NextCard();
        }

        private async void OnWrongClicked(object sender, EventArgs e)
        {
            // Ajouter la carte actuelle à la fin de la liste pour la revoir
            var currentCard = _shuffledCards[_currentIndex];
            _shuffledCards.Add(currentCard);

            await NextCard();
        }

        private async Task NextCard()
        {
            _currentIndex++;
            if (_currentIndex < _shuffledCards.Count)
            {
                ShowCard();
            }
            else
            {
                // Session finished
                var navigationParameter = new Dictionary<string, object>
                {
                    { "correct", _correctCount },
                    { "total", _shuffledCards.Count },
                    { "deckName", _deck.Name }
                };
                await Shell.Current.GoToAsync("LearnResultPage", navigationParameter);
            }
        }

    }
}
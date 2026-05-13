using FlashCard.Models;
using FlashCard.Services;

namespace FlashCard;

public partial class AddCardPage : ContentPage, IQueryAttributable
{
    private Deck _deck;
    private JsonDataService _dataService;
    private List<Deck> _decks;

    public AddCardPage()
    {
        InitializeComponent();
    }

    private Card _cardToEdit;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("deck", out object deckObj) && deckObj is Deck deck)
        {
            _deck = deck;
            Title = $"Ajouter à {_deck.Name}";
        }

        if (query.TryGetValue("dataService", out object serviceObj) && serviceObj is JsonDataService service)
        {
            _dataService = service;
        }

        if (query.TryGetValue("decks", out object decksObj) && decksObj is List<Deck> decks)
        {
            _decks = decks;
        }

        if (query.TryGetValue("card", out object cardObj) && cardObj is Card card)
        {
            _cardToEdit = card;
            Title = "Modifier la carte";
            QuestionEditor.Text = _cardToEdit.Front;
            AnswerEditor.Text = _cardToEdit.Back;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(QuestionEditor.Text) || string.IsNullOrWhiteSpace(AnswerEditor.Text))
        {
            await DisplayAlert("Erreur", "Veuillez remplir la question et la réponse", "OK");
            return;
        }

        if (_deck == null || _dataService == null || _decks == null)
        {
            await DisplayAlert("Erreur", "Données manquantes", "OK");
            return;
        }

        if (_cardToEdit != null)
        {
            _cardToEdit.Front = QuestionEditor.Text.Trim();
            _cardToEdit.Back = AnswerEditor.Text.Trim();
        }
        else
        {
            if (_deck.Cards == null) _deck.Cards = new List<Card>();

            _deck.Cards.Add(new Card
            {
                Front = QuestionEditor.Text.Trim(),
                Back = AnswerEditor.Text.Trim()
            });

            _deck.CardCount = _deck.Cards.Count;
        }

        await _dataService.SaveDecksAsync(_decks);

        await DisplayAlert("Succès", _cardToEdit != null ? "Carte modifiée !" : "Carte ajoutée !", "OK");
        await Shell.Current.GoToAsync("..");
    }
}
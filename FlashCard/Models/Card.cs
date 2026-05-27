namespace FlashCard.Models
{
    public class Card
    {
        public string Front { get; set; } = string.Empty;
        public string Back { get; set; } = string.Empty;
        public bool IsMastered { get; set; } = false;
    }
}

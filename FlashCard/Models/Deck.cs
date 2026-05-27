namespace FlashCard.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int CardCount { get; set; }

        public Deck()
        {
            CreatedDate = DateTime.Now;
        }
        public List<Card> Cards { get; set; } = new List<Card>();

        public override string ToString()
        {
            return $"{Name} ({CardCount} cartes)";
        }
    }
}

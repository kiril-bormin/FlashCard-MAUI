using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Models
{
    public class Deck // définit le modèle d'une deck
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int CardCount { get; set; }

        public Deck()
        {
            CreatedDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Name} ({CardCount} cards)";
        }
    }
}

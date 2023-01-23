using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo.Models
{
    public class Card
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }

    }
}

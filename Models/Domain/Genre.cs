using System.ComponentModel.DataAnnotations;

namespace MyBookStore.Models.Domain
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

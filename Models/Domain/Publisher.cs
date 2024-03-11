using System.ComponentModel.DataAnnotations;

namespace MyBookStore.Models.Domain
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required]
        public string PublisherName { get; set; }
    }
}

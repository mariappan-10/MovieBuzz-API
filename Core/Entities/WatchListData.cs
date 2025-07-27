using Core.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class WatchListData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string imdbId { get; set; } = string.Empty;

        // Foreign key
        public Guid ApplicationUserId { get; set; }

    }
}

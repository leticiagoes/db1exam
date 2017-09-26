using System.ComponentModel.DataAnnotations;

namespace DB1.AvaliacaoTecnica.API.Models
{
    public class CandidateTechnology
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long IdCandidate { get; set; }
        [Required]
        public long IdTechnology { get; set; }
    }
}

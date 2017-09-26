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

    public class CandidateTechnologyDTO
    {
        public long IdTechnology { get; set; }
    }

    public class CandidateTechnologyExcludeDTO
    {
        public CandidateTechnologyExcludeDTO()
        {
            Delete = false;
        }

        [Required]
        public long Id { get; set; }
        [Required]
        public long IdCandidate { get; set; }
        [Required]
        public long IdTechnology { get; set; }
        public bool Delete { get; set; }
    }
}

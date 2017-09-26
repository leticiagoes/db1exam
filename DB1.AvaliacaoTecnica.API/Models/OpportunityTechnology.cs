using System.ComponentModel.DataAnnotations;

namespace DB1.AvaliacaoTecnica.API.Models
{
    public class OpportunityTechnology
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Peso")]
        public int Weight { get; set; }
        [Required]
        [Display(Name = "Vaga")]
        public long IdOpportunity { get; set; }
        [Required]
        [Display(Name = "Tecnologia")]
        public long IdTechnology { get; set; }
    }
}

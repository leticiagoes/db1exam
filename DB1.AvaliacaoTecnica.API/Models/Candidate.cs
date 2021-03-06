﻿using System.ComponentModel.DataAnnotations;

namespace DB1.AvaliacaoTecnica.API.Models
{
    public class Candidate
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Vaga")]
        public long IdOpportunity { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace DB1.AvaliacaoTecnica.API.Models
{
    public class Technology
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
    }
}

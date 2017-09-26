using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB1.AvaliacaoTecnica.API.Models
{
    public class Validate
    {
        public Validate()
        {
            IsValid = true;
        }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
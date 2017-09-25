namespace DB1.AvaliacaoTecnica.API.Models
{
    public class Opportunity
    {
        public long Id { get; set; }
        public string Description { get; set; }
    }

    public class OpportunityTechnology
    {
        public long Id { get; set; }
        public int Weight { get; set; }
        public long IdOpportunity { get; set; }
        public long IdTechnology { get; set; }
    }
}

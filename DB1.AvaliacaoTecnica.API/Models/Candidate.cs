namespace DB1.AvaliacaoTecnica.API.Models
{
    public class Candidate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long IdOpportunity { get; set; }
    }

    public class CandidateTechnology
    {
        public long Id { get; set; }
        public long IdCandidate { get; set; }
        public long IdTechnology { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DB1.AvaliacaoTecnica.API.Models;
using DB1.AvaliacaoTecnica.API.Services;

namespace DB1.AvaliacaoTecnica.API.Controllers
{
    public class CandidateTechnologyController : ApiController
    {
        public IEnumerable<CandidateTechnology> Get()
        {
            CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
            IEnumerable<CandidateTechnology> list = Mapper.ToList<CandidateTechnology>(rep.GetAll());
            return list; 
        }

        public CandidateTechnology Get(long Id)
        {
            CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
            IEnumerable<CandidateTechnology> list = Mapper.ToList<CandidateTechnology>(rep.GetById(Id));
            return list.FirstOrDefault();
        }

        public void Post([FromBody]CandidateTechnology entity)
        {
            CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
            rep.Insert(entity);
        }

        public void Put([FromBody]CandidateTechnology entity)
        {
            CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
            rep.Update(entity);
        }

        public void Delete(long Id)
        {
            CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
            rep.Delete(Id);
        }
    }
}

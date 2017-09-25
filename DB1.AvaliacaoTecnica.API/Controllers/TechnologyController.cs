using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB1.AvaliacaoTecnica.API.Models;

namespace DB1.AvaliacaoTecnica.API.Controllers
{
    [Route("api/[controller]")]
    public class TechnologyController : ApiController
    {
        /*
         * SELECT C.Name, SUM(OT.Weight) AS Score
FROM (Candidate AS C
INNER JOIN CandidateTechnology AS CT ON CT.IdCandidate = C.Id)
INNER JOIN OpportunityTechnology AS OT ON OT.IdTechnology = CT.IdTechnology AND OT.IdOpportunity = C.IdOpportunity
GROUP BY C.Name
ORDER BY C.Name ASC
*/
        // GET api/values
        public IEnumerable<string> Get()
        {
            DAO.Technology dao = new DAO.Technology("C:\\");
            IEnumerable<Technology> list = Mapper.ToList<Technology>(dao.GetAll());
            return new string[] { "value1", "value2" }; 
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

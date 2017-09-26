using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB1.AvaliacaoTecnica.API.Models;
using DB1.AvaliacaoTecnica.API.Services;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Http.Cors;

namespace DB1.AvaliacaoTecnica.API.Controllers
{
    public class ReportCandidateController : ApiController
    {
        [ResponseType(typeof(IEnumerable<ReportCandidate>))]
        [EnableCors(origins: "http://localhost:8250", headers: "*", methods: "*")]
        public HttpResponseMessage Get()
        {
            try
            {
                CandidateRepository rep = new CandidateRepository();
                IEnumerable<ReportCandidate> list = Mapper.ToList<ReportCandidate>(rep.GetScoreByCandidate());
                if (list != null && list.Count() > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                else
                    return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

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
    public class CandidateTechnologyController : ApiController
    {
        [ResponseType(typeof(IEnumerable<CandidateTechnology>))]
        [EnableCors(origins: "http://localhost:8250", headers: "*", methods: "*")]
        public HttpResponseMessage Get()
        {
            try
            {
                CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
                IEnumerable<CandidateTechnology> list = Mapper.ToList<CandidateTechnology>(rep.GetAll());
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

        [ResponseType(typeof(IEnumerable<long>))]
        [EnableCors(origins: "http://localhost:8250", headers: "*", methods: "*")]
        public HttpResponseMessage Get([Bind(Include = "Id")]long Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
                    IEnumerable<CandidateTechnologyDTO> list = Mapper.ToList<CandidateTechnologyDTO>(rep.GetAllByCandidate(Id));
                    if (list != null && list.Count() > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, list.Select(l => l.IdTechnology));
                    else
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [EnableCors(origins: "http://localhost:8250", headers: "*", methods: "*")]
        public HttpResponseMessage Post([Bind(Exclude = "Id")][FromBody]List<CandidateTechnologyExcludeDTO> list)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
                    foreach (CandidateTechnologyExcludeDTO entity in list) {
                        CandidateTechnology ent = new CandidateTechnology { Id = 0, IdCandidate = entity.IdCandidate, IdTechnology = entity.IdTechnology };
                        if(entity.Delete)
                            rep.DeleteByCandidate(ent);
                        else
                            rep.Insert(ent);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "Operação efetuada com sucesso!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [EnableCors(origins: "http://localhost:8250", headers: "*", methods: "*")]
        public HttpResponseMessage Put([FromBody]CandidateTechnology entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
                    rep.Update(entity);
                    return Request.CreateResponse(HttpStatusCode.OK, "Operação efetuada com sucesso!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [EnableCors(origins: "http://localhost:8250", headers: "*", methods: "*")]
        public HttpResponseMessage Delete([FromBody]List<CandidateTechnology> list)
        {
            try
            {
                foreach (CandidateTechnology entity in list)
                {
                    CandidateTechnologyRepository rep = new CandidateTechnologyRepository();
                    rep.DeleteByCandidate(entity);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Operação efetuada com sucesso!");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

}

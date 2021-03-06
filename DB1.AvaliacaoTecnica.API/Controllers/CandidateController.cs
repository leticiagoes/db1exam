﻿using System;
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
    public class CandidateController : ApiController
    {
        [ResponseType(typeof(IEnumerable<Candidate>))]
        [EnableCors(origins: "http://localhost:8250", headers: "*", methods: "*")]
        public HttpResponseMessage Get()
        {
            try
            {
                CandidateRepository rep = new CandidateRepository();
                IEnumerable<Candidate> list = Mapper.ToList<Candidate>(rep.GetAll());
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

        [ResponseType(typeof(Candidate))]
        [EnableCors(origins: "http://localhost:8250", headers: "*", methods: "*")]
        public HttpResponseMessage Get([Bind(Include = "Id")]long Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CandidateRepository rep = new CandidateRepository();
                    IEnumerable<Candidate> list = Mapper.ToList<Candidate>(rep.GetById(Id));
                    if (list != null && list.Count() > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, list.FirstOrDefault());
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
        public HttpResponseMessage Post([Bind(Exclude = "Id")][FromBody]Candidate entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CandidateRepository rep = new CandidateRepository();
                    Validate repValid = rep.ValidateInsert(entity);
                    if (repValid.IsValid)
                    {
                        int Id = rep.Insert(entity);
                        return Request.CreateResponse(HttpStatusCode.OK, new { Id = Id, Message = "Operação efetuada com sucesso!" });
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, repValid.Message);
                    }
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
        public HttpResponseMessage Put([FromBody]Candidate entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CandidateRepository rep = new CandidateRepository();
                    Validate repValid = rep.ValidateUpdate(entity);
                    if (repValid.IsValid)
                    {
                        rep.Update(entity);
                        return Request.CreateResponse(HttpStatusCode.OK, "Operação efetuada com sucesso!");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, repValid.Message);
                    }
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
        public HttpResponseMessage Delete(long Id)
        {
            try
            {
                if (Id > 0)
                {
                    CandidateRepository rep = new CandidateRepository();
                    Validate repValid = rep.ValidateDelete(Id);
                    if (repValid.IsValid)
                    {
                        rep.Delete(Id);
                        return Request.CreateResponse(HttpStatusCode.OK, "Operação efetuada com sucesso!");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, repValid.Message);
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id inválido!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

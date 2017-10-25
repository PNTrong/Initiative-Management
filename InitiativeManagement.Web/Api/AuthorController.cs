using AutoMapper;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Service;
using InitiativeManagement.Web.Infrastructure.Core;
using InitiativeManagement.Web.Infrastructure.Extensions;
using InitiativeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace InitiativeManagement.Web.Api
{
    [RoutePrefix("api/author")]
    public class AuthorController : ApiControllerBase
    {
        #region Initialize

        private IAuthorService _authorService;

        public AuthorController(IErrorService errorService, IAuthorService authorService)
            : base(errorService)
        {
            this._authorService = authorService;
        }

        #endregion Initialize

        [Route("getallauthor")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _authorService.GetAll();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _authorService.GetById(id);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getallbykeyword")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _authorService.GetAll(keyword);

                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newAuthor = new Author();
                    _authorService.Add(newAuthor);
                    _authorService.Save();

                    var responseData = _authorService.Add(newAuthor);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, Author author)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbAuthor = _authorService.GetById(author.Id);
                    _authorService.Update(dbAuthor);
                    _authorService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbAuthor);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldAuthor = _authorService.Delete(id);
                    _authorService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, oldAuthor);
                }

                return response;
            });
        }
    }
}
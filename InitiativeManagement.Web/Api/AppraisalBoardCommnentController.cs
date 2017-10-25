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
    [RoutePrefix("api/AppraisalBoard")]
    public class AppraisalBoardCommnentController : ApiControllerBase
    {
        #region Initialize

        private IAppraisalBoardCommnentService _appraisalBoardCommnentService;
        private IAppraisalBoardMemberCommnentService _appraisalBoardMemberCommnentService;

        public AppraisalBoardCommnentController(IErrorService errorService, IAppraisalBoardCommnentService appraisalBoardCommnentService)
            : base(errorService)
        {
            this._appraisalBoardCommnentService = appraisalBoardCommnentService;
        }

        #endregion Initialize

        [Route("getallAppraisalBoard")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _appraisalBoardCommnentService.GetAll();
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
                var model = _appraisalBoardCommnentService.GetById(id);
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
                var model = _appraisalBoardCommnentService.GetAll(keyword);

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
                    var newAppraisalBoardCommnent = new AppraisalBoardCommnent();
                    _appraisalBoardCommnentService.Add(newAppraisalBoardCommnent);
                    _appraisalBoardCommnentService.Save();

                    var responseData = _appraisalBoardCommnentService.Add(newAppraisalBoardCommnent);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, AppraisalBoardCommnent appraisalBoardCommnent)
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
                    var dbAppraisalBoardCommnent = _appraisalBoardCommnentService.GetById(appraisalBoardCommnent.Id);
                    _appraisalBoardCommnentService.Update(dbAppraisalBoardCommnent);
                    _appraisalBoardCommnentService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbAppraisalBoardCommnent);
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
                var olddbAppraisalBoardMemberCommnent = _appraisalBoardMemberCommnentService.FindById(id);
                HttpResponseMessage response = null;
                if (olddbAppraisalBoardMemberCommnent != null)
                {
                }
                else
                {
                    var olddbAppraisalBoardCommnent = _appraisalBoardCommnentService.GetById(id);
                    olddbAppraisalBoardCommnent.IsDeactive = false;
                    _appraisalBoardCommnentService.Update(olddbAppraisalBoardCommnent);
                    _appraisalBoardCommnentService.Save();
                    // response = request.CreateResponse(HttpStatusCode.Created, oldFieldGroup);
                }

                return response;
            });
        }
    }
}
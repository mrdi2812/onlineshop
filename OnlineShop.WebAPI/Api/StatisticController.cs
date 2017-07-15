using OnlineShop.WebAPI.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShop.Service;

namespace OnlineShop.WebAPI.Api
{
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        private IStatisticService _statisticService;
        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            this._statisticService = statisticService;
        }
        [Route("getrevenue")]
        [HttpGet]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request,string fromDate,string toDate)
        {
            return CreateHttpResponse(request, () =>
             {
                 var model = _statisticService.GetRevenueStatistic(fromDate, toDate);
                 HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                 return response;
             });
        } 
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using POC.BusinessObjects;
using POC.IProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POC.WebApi.Controllers
{
    [RoutePrefix("api/information")]
    public class InformationController : ApiController
    {
        private IInformationProvider _informationProvider;

        public InformationController(IInformationProvider informationProvider)
        {
            _informationProvider = informationProvider;
        }

        [HttpPost, Route("save")]
        public IHttpActionResult SaveInformation([FromBody] PostObject<object> postObject)
        {
            JsonSerializerSettings serSettings = new JsonSerializerSettings();
            serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            INFORMATION info = JsonConvert.DeserializeObject<INFORMATION>(postObject.Data.ToString(), serSettings);
            var result = this._informationProvider.AddInformation(info);
            return Ok(result);
        }
    }
}

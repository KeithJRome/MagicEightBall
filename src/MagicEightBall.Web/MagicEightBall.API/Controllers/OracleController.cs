using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Threading;
using System.Web.Http;

namespace MagicEightBall.API.Controllers
{
    public class OracleController : ApiController
    {
        // GET api/askQuestion/question
        [SwaggerOperation("AskQuestion")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [HttpGet()]
        public OracleResponse AskQuestion(string question, bool forceFastFail = false, bool forceSlowFail = false)
        {
            // WARNING: NEVER DO THIS IN A PRODUCTION APP!!!!

            // allow client to simulate a catastrophic server error situation
            if (forceFastFail)
                throw new Exception("Simulating a service crash");

            // allow client to simulate a server timeout
            if (forceSlowFail)
                Thread.Sleep(TimeSpan.FromMinutes(1));



            return Oracle.GetRandomResponse();
        }
    }
}

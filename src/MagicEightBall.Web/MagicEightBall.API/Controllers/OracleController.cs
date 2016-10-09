using Swashbuckle.Swagger.Annotations;
using System.Net;
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
        public OracleResponse AskQuestion(string question)
        {
            return Oracle.GetRandomResponse();
        }
    }
}

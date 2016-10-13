using Refit;
using System.Threading.Tasks;

namespace MagicEightBall.Mobile
{
    public class OracleResponse
    {
        public string Response { get; set; }
        public string Disposition { get; set; }
    }

    public interface IOracle
    {
        // http://magic-8-ball-api.azurewebsites.net/api/oracle/askquestion?question=testing
        //{
        //  "Response": "Concentrate and ask again",
        //  "Disposition": "neutral"
        //}

        [Get("/api/oracle/askquestion?question={question}")]
        Task<OracleResponse> AskQuestion(string question);
    }
}

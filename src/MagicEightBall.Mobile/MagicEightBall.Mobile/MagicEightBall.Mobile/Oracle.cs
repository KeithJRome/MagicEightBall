using System.Threading.Tasks;

namespace MagicEightBall.Mobile
{
    public class Oracle
    {
        IOracle _remote;

        public Oracle(string serverName)
        {
            _remote = Refit.RestService.For<IOracle>(serverName);
        }

        public async Task<OracleResponse> AskQuestion(string question)
        {
            // do some error checking here, and retry logic
            return await _remote.AskQuestion(question);
        }
    }
}

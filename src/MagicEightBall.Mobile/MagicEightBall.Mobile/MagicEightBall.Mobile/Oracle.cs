using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MagicEightBall.Mobile
{
    public class Oracle
    {
        IOracle _remote;

        public Oracle(string serverName)
        {
            var client = new System.Net.Http.HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            client.BaseAddress = new Uri(serverName);
            _remote = Refit.RestService.For<IOracle>(client);
        }

        public async Task<OracleResponse> AskQuestion(string question)
        {
            var triesLeft = 5;
            Exception savedException = null;
            while (triesLeft > 0)
            {
                try
                {
                    //var fast = true;// triesLeft > 3;
                    //var slow = triesLeft > 1 && !fast;
                    //if (fast) Debug.WriteLine("Triggering Fast Fail");
                    //if (slow) Debug.WriteLine("Triggering Slow Fail");
                    var response = await _remote.AskQuestion(question /*, fast, slow*/);
                    return response;
                }
                catch (Exception ex)
                {
                    savedException = ex;
                    while (savedException.InnerException != null)
                        savedException = savedException.InnerException;
                    Debug.WriteLine(savedException.GetType().Name + ": " + savedException.Message);
                    triesLeft--;
                }
            }

            throw savedException;
        }
    }
}

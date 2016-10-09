using System.Collections.Generic;
using System.Security.Cryptography;

namespace MagicEightBall.API.Controllers
{
    public static class Oracle
    {
        static RNGCryptoServiceProvider _rng;
        static byte[] _rngBuffer;
        static List<OracleResponse> _cannedResponses;

        static void AddResponse(Disposition disposition, string response)
        {
            _cannedResponses.Add(
                new OracleResponse
                {
                    Disposition = disposition,
                    Response = response
                }
                );
        }

        static Oracle()
        {
            _rng = new RNGCryptoServiceProvider();
            _rngBuffer = new byte[1];

            _cannedResponses = new List<OracleResponse>();
            AddResponse(Disposition.Positive, "It is certain");
            AddResponse(Disposition.Positive, "It is decidedly so");
            AddResponse(Disposition.Positive, "Without a doubt");
            AddResponse(Disposition.Positive, "Yes, definitely");
            AddResponse(Disposition.Positive, "You may rely on it");
            AddResponse(Disposition.Positive, "As I see it, yes");
            AddResponse(Disposition.Positive, "Most likely");
            AddResponse(Disposition.Positive, "Outlook good");
            AddResponse(Disposition.Positive, "Yes");
            AddResponse(Disposition.Positive, "Signs point to yes");
            AddResponse(Disposition.Neutral, "Reply hazy try again");
            AddResponse(Disposition.Neutral, "Ask again later");
            AddResponse(Disposition.Neutral, "Better not tell you now");
            AddResponse(Disposition.Neutral, "Cannot predict now");
            AddResponse(Disposition.Neutral, "Concentrate and ask again");
            AddResponse(Disposition.Negative, "Don't count on it");
            AddResponse(Disposition.Negative, "My reply is no");
            AddResponse(Disposition.Negative, "My sources say no");
            AddResponse(Disposition.Negative, "Outlook not so good");
            AddResponse(Disposition.Negative, "Very doubtful");
        }

        public static OracleResponse GetRandomResponse()
        {
            _rngBuffer[0] = byte.MaxValue;
            while (_rngBuffer[0] > _cannedResponses.Count - 1)
                _rng.GetBytes(_rngBuffer);
            var randomizedResponse = _cannedResponses[_rngBuffer[0]];
            return randomizedResponse;
        }
    }
}
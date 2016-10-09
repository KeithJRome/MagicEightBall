namespace MagicEightBall.API.Controllers
{
    public sealed class OracleResponse
    {
        public string Response { get; internal set; }
        public string Disposition { get; internal set; }

        internal OracleResponse() { }
    }
}
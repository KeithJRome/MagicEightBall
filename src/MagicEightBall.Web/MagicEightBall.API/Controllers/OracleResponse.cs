namespace MagicEightBall.API.Controllers
{
    public sealed class OracleResponse
    {
        public string Response { get; internal set; }
        public Disposition Disposition { get; internal set; }

        internal OracleResponse() { }
    }
}
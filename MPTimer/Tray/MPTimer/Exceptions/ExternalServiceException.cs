using RestSharp;

namespace MPTimer.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public IRestResponse Response { get; }

        public ExternalServiceException(IRestResponse response)
        {
            Response = response;
        }
    }
}

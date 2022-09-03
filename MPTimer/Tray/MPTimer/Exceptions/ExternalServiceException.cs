using RestSharp;

namespace MPTimer.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public RestResponse Response { get; }

        public ExternalServiceException(RestResponse response)
        {
            Response = response;
        }
    }
}

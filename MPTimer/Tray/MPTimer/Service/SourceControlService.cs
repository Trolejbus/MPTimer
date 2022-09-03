using Microsoft.Extensions.Configuration;
using MPTimer.Exceptions;
using MPTimer.Interfaces;
using MPTimer.Models;
using RestSharp;

namespace MPTimer.Service
{
    internal class SourceControlService : ISourceControlService
    {
        private readonly IConfiguration _configuration;
        private readonly RestClient client;
        private readonly string backendUrl;

        public SourceControlService(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new RestClient();
            backendUrl = _configuration["BackendUrl"];
        }

        public void ChangeBranch(Guid sourceControlId, string branchName)
        {
            var client = new RestClient();
            RestRequest request = new RestRequest($"{backendUrl}/api/sourceControl/{sourceControlId}/changeBranch", Method.Put);
            request = request.AddQueryParameter("branchName", branchName);
            var response = client.Execute(request);
        }

        public async Task<IEnumerable<TraySourceControl>> GetWatched()
        {
            var request = new RestRequest($@"{backendUrl}/api/sourceControls", Method.Get);
            var response = await client.ExecuteAsync<IEnumerable<TraySourceControl>>(request).ConfigureAwait(false);
            if (!response.IsSuccessful)
            {
                throw new ExternalServiceException(response);
            }

            return response.Data;
        }
    }
}

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

        public SourceControlService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ChangeBranch(Guid sourceControlId, string branchName)
        {
            var client = new RestClient();
            IRestRequest request = new RestRequest($"{_configuration["BackendUrl"]}/api/sourceControl/{sourceControlId}/changeBranch", Method.PUT);
            request = request.AddQueryParameter("branchName", branchName);
            var response = client.Execute(request);
        }

        public async Task<IEnumerable<TraySourceControl>> GetWatched()
        {
            var client = new RestClient();
            var request = new RestRequest($"{_configuration["BackendUrl"]}/api/sourceControls", Method.GET);
            var response = await client.ExecuteAsync<IEnumerable<TraySourceControl>>(request);
            if (!response.IsSuccessful)
            {
                throw new ExternalServiceException(response);
            }

            return response.Data;
        }
    }
}

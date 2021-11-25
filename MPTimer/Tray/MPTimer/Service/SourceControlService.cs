using Microsoft.Extensions.Configuration;
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
            var request = new RestRequest($"{_configuration["BackendUrl"]}/api/sourceControl/{sourceControlId}/changeBranch/{branchName}", Method.PUT);
            var response = client.Execute(request);
        }

        public async Task<IEnumerable<TraySourceControl>> GetWatched()
        {
            var client = new RestClient();
            var request = new RestRequest($"{_configuration["BackendUrl"]}/api/sourceControls", Method.GET);
            var response = client.Execute<IEnumerable<TraySourceControl>>(request);
            await Task.CompletedTask;
            return response.Data;
        }
    }
}

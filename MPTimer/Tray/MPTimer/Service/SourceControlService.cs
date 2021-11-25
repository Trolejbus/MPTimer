using Microsoft.Extensions.Configuration;
using MPTimer.Interfaces;
using MPTimer.Models;

namespace MPTimer.Service
{
    internal class SourceControlService : ISourceControlService
    {
        private readonly IConfiguration _configuration;

        public SourceControlService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<TraySourceControl>> GetWatched()
        {
            return await Task.FromResult(new List<TraySourceControl>()
            {
                new TraySourceControl(Guid.NewGuid(), "Creditboard", @"C:\sources\creditboard"),
            });

            /*var client = new RestClient();
            var request = new RestRequest($"{_configuration["BackendUrl"]}/api/workspaceEvent", Method.POST).AddJsonBody(workspaceEvent);
            var response = await client.ExecuteAsync<TrayWorkspaceEvent>(request);
            return response.Data;*/
        }
    }
}

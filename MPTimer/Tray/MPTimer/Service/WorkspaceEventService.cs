using Microsoft.Extensions.Configuration;
using MPTimer.Interfaces;
using MPTimer.Models;
using RestSharp;

namespace MPTimer.Service
{
    public class WorkspaceEventService : IWorkspaceEventService
    {
        private readonly IConfiguration _configuration;

        public WorkspaceEventService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TrayWorkspaceEvent> Create(TrayWorkspaceEvent workspaceEvent)
        {
            var client = new RestClient();
            var request = new RestRequest($"{_configuration["BackendUrl"]}/api/workspaceEvent", Method.POST).AddJsonBody(workspaceEvent);
            var response = await client.ExecuteAsync<TrayWorkspaceEvent>(request);
            return response.Data;
        }

        public async Task<TrayWorkspaceEvent> Update(TrayWorkspaceEvent workspaceEvent)
        {
            var client = new RestClient();
            var request = new RestRequest($"{_configuration["BackendUrl"]}/api/workspaceEvent/{workspaceEvent.Id}", Method.PUT).AddJsonBody(workspaceEvent);
            var response = await client.ExecuteAsync<TrayWorkspaceEvent>(request);
            return response.Data;
        }
    }
}

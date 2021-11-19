using AutoMapper;
using MPTimerAgent.Entities;
using MPTimerAgent.Models;
using MPTimerWeb.Dtos.Agent;
using MPTimerWeb.Dtos.WorkTask;
using MPTimerWorkTask.Models;

namespace MPTimerWeb
{
    public class MPTimerWebProfile : Profile
    {
        public MPTimerWebProfile()
        {
            CreateMap<WorkTask, WorkTaskDto>()
                .ReverseMap();
            CreateMap<Agent, AgentDto>()
                .ReverseMap();
            CreateMap<AgentState, AgentStateDto>();
        }
    }
}

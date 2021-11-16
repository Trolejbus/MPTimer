using AutoMapper;
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
        }
    }
}

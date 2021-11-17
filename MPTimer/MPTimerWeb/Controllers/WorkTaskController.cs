using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MPTimerWeb.Dtos.WorkTask;
using MPTimerWorkTask.Interfaces;
using MPTimerWorkTask.Models;

namespace MPTimerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTaskController : Controller
    {
        private IWorkTaskRepository _repository;
        private readonly IMapper _mapper;

        public WorkTaskController(IWorkTaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<WorkTaskDto>> GetAll()
        {
            var checks = await _repository.GetAll();
            return _mapper.Map<IEnumerable<WorkTaskDto>>(checks);
        }

        [HttpPost]
        public async Task<WorkTaskDto> Add(WorkTaskDto workTaskDto)
        {
            var model = _mapper.Map<WorkTask>(workTaskDto);
            var checks = await _repository.Add(model);
            return _mapper.Map<WorkTaskDto>(checks);
        }
    }
}

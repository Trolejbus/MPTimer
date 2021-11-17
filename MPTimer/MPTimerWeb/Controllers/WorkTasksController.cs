using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MPTimerWeb.Dtos.WorkTask;
using MPTimerWorkTask.Interfaces;
using MPTimerWorkTask.Models;

namespace MPTimerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTasksController : Controller
    {
        private IWorkTaskRepository _repository;
        private readonly IMapper _mapper;

        public WorkTasksController(IWorkTaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/api/workTask/{id}")]
        public async Task<WorkTaskDto> GetById(Guid id)
        {
            var checks = await _repository.GetById(id);
            return _mapper.Map<WorkTaskDto>(checks);
        }

        [HttpGet("/api/workTasks")]
        public async Task<IEnumerable<WorkTaskDto>> GetAll()
        {
            var checks = await _repository.GetAll();
            return _mapper.Map<IEnumerable<WorkTaskDto>>(checks);
        }

        [HttpPost("/api/workTask")]
        public async Task<WorkTaskDto> Add(WorkTaskDto workTaskDto)
        {
            var model = _mapper.Map<WorkTask>(workTaskDto);
            var checks = await _repository.Add(model);
            return _mapper.Map<WorkTaskDto>(checks);
        }

        [HttpPut("/api/workTask/{id}")]
        public async Task<WorkTaskDto> Add(Guid id, WorkTaskDto workTaskDto)
        {
            var model = _mapper.Map<WorkTask>(workTaskDto);
            var checks = await _repository.Update(id, model);
            return _mapper.Map<WorkTaskDto>(checks);
        }

        [HttpDelete("/api/workTask/{id}")]
        public async Task Remove(Guid id)
        {
            await _repository.Remove(id);
        }
    }
}

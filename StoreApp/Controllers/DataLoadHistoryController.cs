using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repository.IRepository;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataLoadHistoryController : ControllerBase
    {
        private readonly IDataLoadHistoryRepository _repository;

        public DataLoadHistoryController(IDataLoadHistoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<DataLoadHistoryDTO>>> GetAll()
        {
            var result = await _repository.GetAllDataLoadHistoriesAsync();
            return Ok(result);
        }

        [HttpGet("latest")]
        public async Task<ActionResult<DataLoadHistoryDTO>> GetLatest()
        {
            var result = await _repository.GetLatestDataLoadHistoryAsync();
            return Ok(result);
        }
    }
}

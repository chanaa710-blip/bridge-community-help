using Bridge.Core.Models;
using Bridge.Core.Resources;
using Bridge.Core.Services;
using Bridge.Service;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
      public AnswerController(IAnswerService answerService) 
        { 
            _answerService = answerService;     
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(AnswerResource entity)
        {
            return Ok(await _answerService.Add(entity));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerResource>>> GetAll()
        {
            var answers = await _answerService.GetAll();
            return Ok(answers);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(Guid id)
        {
            return Ok(await _answerService.DeleteById(id));
        }

        [HttpGet("request/{requestId}")]
        public async Task<ActionResult<IEnumerable<AnswerResource>>> GetAnswersByRequestId(Guid requestId)
        {
            var answers = await _answerService.GetAnswersByRequestId(requestId);
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerResource?>> GetById(Guid id)
        {
            var answer = await _answerService.GetById(id);
            return Ok(answer);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateAnswer(AnswerResource entity)
        {
            return Ok(await _answerService.Update(entity));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAnswersByUserId(Guid userId)
        {
            var answers = await _answerService.GetAnswersByUserId(userId);
            return Ok(answers);
        }
    }
}

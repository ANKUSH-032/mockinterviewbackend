using BluckImport.Core.ClsResponce;
using BluckImport.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mockinterview.core.Interface;
using static mockinterview.core.Model.Answer;

namespace mockinterviewbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _answerRepository;
        public AnswerController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }
        [HttpPost("answer")]
        public async Task<IActionResult> AnswerInsert([FromBody] AnswerInsert answerInsert)
        {
            Response response = await _answerRepository.AddAnswer(answerInsert);

            if (!response.Status)
            {
                return BadRequest(new
                {
                    status = false,
                    message = response.Message
                });
            }
            return Ok(response);
        }
    }

}

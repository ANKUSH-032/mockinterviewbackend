using BluckImport.Core.ClsResponce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mockinterview.core.common;
using mockinterview.core.Interface;
using mockinterview.core.Model;
using Mockinterview.Generic;

namespace mockinterviewbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {

        private readonly ICandidateRepository _CandidateRepository;
        private readonly IConfiguration _configuration;
        public CandidateController(ICandidateRepository CandidateRepository, IConfiguration configuration)
        {
            _CandidateRepository = CandidateRepository;
            _configuration = configuration;

        }
        [HttpPost, Route("insert")]
        public async Task<IActionResult> CandidateInsert([FromBody] CandidateInsert CandidateInsert)
        {
            try
            {
                Authenticates.CreatePasswordHash(CandidateInsert.Password, out byte[] passwordHash, out byte[] passwordSalt);
                CandidateInsert.PasswordHash = passwordHash;
                CandidateInsert.PasswordSalt = passwordSalt;

                var res = await _CandidateRepository.CandidateInsert(CandidateInsert);

                return res.Status ? StatusCode(StatusCodes.Status201Created, res) : StatusCode(StatusCodes.Status409Conflict, res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
        [HttpPost, Route("list")]
        public async Task<IActionResult> CandidateList([FromBody] JqueryDataTable CandidateList)
        {
            try
            {
                var res = await _CandidateRepository.CandidateList(CandidateList);
                return res.Status ? StatusCode(StatusCodes.Status200OK, res) : StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }

        [HttpPost, Route("update")]
        public async Task<IActionResult> CandidateUpdate([FromBody] CandidateUpdate CandidateUpdate)
        {
            try
            {
                var responce = await _CandidateRepository.CandidateUpdate(CandidateUpdate);

                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpGet, Route("get")]

        public async Task<IActionResult> CandidateGet(string CandidateId)
        {
            try
            {
                var responce = await _CandidateRepository.CandidateGet(CandidateId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpDelete, Route("delete")]
        public async Task<IActionResult> CandidateDelete(string CandidateId)
        {
            try
            {
                var responce = await _CandidateRepository.CandidateDelete(CandidateId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {

                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
    }
    
}

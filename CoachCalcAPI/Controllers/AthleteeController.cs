using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoachCalcAPI.Data;
using CoachCalcAPI.Models;
using Microsoft.AspNetCore.Cors;
using CoachCalcAPI.Repository.IRepository;
using AutoMapper;
using System.Net;
using CoachCalcAPI.Models.Dto;
using CoachCalcAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CoachCalcAPI.Controllers
{
    [Route("api/Athletee")]
    [ApiController]
    [Authorize(Roles = "admin, customer")]
    public class AthleteeController : ControllerBase
    {
        protected APIResponse _response;
        private ILogger<AthleteeController> _logger;
        private readonly IAthleteeRepository _dbAthletee;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AthleteeController(IAthleteeRepository dbAthletee, ILogger<AthleteeController> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbAthletee = dbAthletee;
            _logger = logger;
            _mapper = mapper;
            this._response = new();
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAthleteess()
        {
            try
            {
                _logger.LogInformation("Getting All Athletees");
                string userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                IEnumerable<Athletee> athleteeList = await _dbAthletee.GetAllAsync(x => x.UserId == userId);
                _response.Result = _mapper.Map<List<AthleteeDTO>>(athleteeList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMesseges = new List<string> { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}", Name = "GetAthletee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAthletee(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation("Get Athletee error with Id: " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var athletee = await _dbAthletee.GetAsync(x => x.Id == id);

                if (athletee == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<AthleteeDTO>(athletee);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMesseges = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateAthletee([FromBody] AthleteeCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Athletee athletee = _mapper.Map<Athletee>(createDTO);

                await _dbAthletee.CreateAsync(athletee);
                _response.Result = _mapper.Map<AthleteeDTO>(athletee);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetAthletee", new { id = athletee.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMesseges = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteAthletee")]
        public async Task<ActionResult<APIResponse>> DeleteAthletee(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var athletee = await _dbAthletee.GetAsync(x => x.Id == id);
                if (athletee == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbAthletee.RemoveAsync(athletee);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMesseges = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateAthletee")]
        public async Task<ActionResult<APIResponse>> UpdateAthletee(int id, [FromBody] AthleteeUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Athletee model = _mapper.Map<Athletee>(updateDTO);

                await _dbAthletee.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMesseges = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}

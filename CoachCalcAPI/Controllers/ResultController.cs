using AutoMapper;
using CoachCalcAPI.Models;
using CoachCalcAPI.Models.Dto;
using CoachCalcAPI.Repository.IRepository;
using CoachCalcAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoachCalcAPI.Controllers
{
    [Route("api/Result")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        protected APIResponse _response;
        private ILogger<ResultController> _logger;
        private readonly IResultRepository _dbResult;
        private readonly IMapper _mapper;

        public ResultController(IResultRepository dbResult, ILogger<ResultController> logger, IMapper mapper)
        {
            _dbResult = dbResult;
            _logger = logger;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetResults()
        {
            try
            {
                _logger.LogInformation("Getting All books");
                IEnumerable<Result> bookList = await _dbResult.GetAllAsync();
                _response.Result = _mapper.Map<List<ResultDTO>>(bookList);
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


        [HttpGet("{id:int}", Name = "GetResult")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetResult(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation("Get Result error with Id: " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var result = await _dbResult.GetAsync(x => x.Id == id);

                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<ResultDTO>(result);
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
        public async Task<ActionResult<APIResponse>> CreateResult([FromBody] ResultCreateDTO createDTO)
        {
            try
            {
                if (await _dbResult.GetAsync(x => x.Date == createDTO.Date && x.AthleteeId == createDTO.AthleteeId &&
                                            x.Value == createDTO.Value) != null)
                {
                    ModelState.AddModelError("Message", "Exercise already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Result result = _mapper.Map<Result>(createDTO);

                await _dbResult.CreateAsync(result);
                _response.Result = _mapper.Map<ResultDTO>(result);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetResult", new { id = result.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteResult")]
        public async Task<ActionResult<APIResponse>> DeleteResult(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var result = await _dbResult.GetAsync(x => x.Id == id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbResult.RemoveAsync(result);
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
        [HttpPut("{id:int}", Name = "UpdateResult")]
        public async Task<ActionResult<APIResponse>> UpdateResult(int id, [FromBody] ResultUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Result model = _mapper.Map<Result>(updateDTO);

                await _dbResult.UpdateAsync(model);
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

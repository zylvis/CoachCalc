using AutoMapper;
using CoachCalcAPI.Models;
using CoachCalcAPI.Models.Dto;
using CoachCalcAPI.Repository.IRepository;
using CoachCalcAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace CoachCalcAPI.Controllers
{
    [Route("api/Exercise")]
    [ApiController]
    [Authorize(Roles = "admin, customer")]
    public class ExerciseController : ControllerBase
    {
        protected APIResponse _response;
        private ILogger<ExerciseController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExerciseRepository _dbExercise;
        private readonly IMapper _mapper;

        public ExerciseController(IExerciseRepository dbExercise, ILogger<ExerciseController> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbExercise = dbExercise;
            _logger = logger;
            _mapper = mapper;
            this._response = new();
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetExercises()
        {
            try
            {
                _logger.LogInformation("Getting All exercises");
                string userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                IEnumerable<Exercise> exerciseList = await _dbExercise.GetAllAsync(x=>x.UserId == userId);
                _response.Result = _mapper.Map<List<ExerciseDTO>>(exerciseList);
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


        [HttpGet("{id:int}", Name = "GetExercise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetExercise(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation("Get Exercise error with Id: " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var exercise = await _dbExercise.GetAsync(x => x.Id == id);

                if (exercise == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<ExerciseDTO>(exercise);
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
        public async Task<ActionResult<APIResponse>> CreateBook([FromBody] ExerciseCreateDTO createDTO)
        {
            try
            {
                if (await _dbExercise.GetAsync(x => x.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Message", "Exercise already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO.MetricType != MetricTypes.Number && createDTO.MetricType != MetricTypes.Time)
                {
                    ModelState.AddModelError("Message", "Metric type is not of acceptbles strings: 'Number' or 'Time'");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Exercise exercise = _mapper.Map<Exercise>(createDTO);

                await _dbExercise.CreateAsync(exercise);
                _response.Result = _mapper.Map<ExerciseDTO>(exercise);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetExercise", new { id = exercise.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteExercise")]
        public async Task<ActionResult<APIResponse>> DeleteExercise(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var exercise = await _dbExercise.GetAsync(x => x.Id == id);
                if (exercise == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbExercise.RemoveAsync(exercise);
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
        [HttpPut("{id:int}", Name = "UpdateExercise")]
        public async Task<ActionResult<APIResponse>> UpdateExercise(int id, [FromBody] ExerciseUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Exercise model = _mapper.Map<Exercise>(updateDTO);

                await _dbExercise.UpdateAsync(model);
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


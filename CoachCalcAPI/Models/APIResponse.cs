using System.Net;

namespace CoachCalcAPI.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMesseges { get; set; } = new List<string>();
        public object Result { get; set; }
    }
}

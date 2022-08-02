using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using todoapifunc.Attribute;

namespace todoapifunc.Controllers
{    
    [Route("[controller]")]
    [ApiController]
    [AllowCrossSite]
    [EnableCors()]
    public class JsonController : ControllerBase
    {
        // GET: api/json
        [HttpGet]
        public IActionResult GetTodoItems()
        {
            string hardCodedJson = "{'body': 'Hello!'}";
            int hardCodedStatusCode = 200;
            string hardCodedContentType = "application/json";

            Newtonsoft.Json.Linq.JObject job = Newtonsoft.Json.Linq.JObject.Parse(hardCodedJson);
            Microsoft.AspNetCore.Mvc.ContentResult contRes = this.Content(job.ToString());
            contRes.StatusCode = hardCodedStatusCode;
            contRes.ContentType = hardCodedContentType;

            return contRes;
        }

        // GET: api/json/5
        [HttpGet("{id}")]
        public ActionResult GetTodoItem(int id)
        {
            string hardCodedJson = "{'body': '" + id.ToString() + " '}";
            int hardCodedStatusCode = 200;

            Newtonsoft.Json.Linq.JObject job = Newtonsoft.Json.Linq.JObject.Parse(hardCodedJson);
            Microsoft.AspNetCore.Mvc.ContentResult contRes = this.Content(job.ToString());
            contRes.StatusCode = hardCodedStatusCode;

            return contRes;
        }
    }
}

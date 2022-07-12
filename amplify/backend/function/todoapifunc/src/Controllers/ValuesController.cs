using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace todoapifunc.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET /values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET /values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Value is: " + id.ToString();
        }

        // POST /values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            Console.WriteLine("Post");
        }

        // PUT /values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE /values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

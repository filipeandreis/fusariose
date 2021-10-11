using fusariose_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace fusariose_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            List<DataModel> data = new();

            data = MockFactory.MockFactory.GerarListaDados();

            var dataList = JsonConvert.SerializeObject(data);

            return Ok(dataList);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DataModel data)
        {
            return Ok(data.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]DataModel data)
        {
            return Ok(data.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(id);
        }
    }
}

using fusariose_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using fusariose.Domain.Repository;
using fusariose.Application;
using fusariose.Repository;
using fusariose.Application.DTO;

namespace fusariose_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IDataRepository dataRepository;
        private DataApplication dataApplication;

        public DataController()
        {
            dataRepository = new DataRepository();
            dataApplication = new DataApplication(dataRepository);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allData = dataApplication.GetAll();

            List<DataModel> listData = new();

            foreach (var dataDTO in allData)
            {
                listData.Add(new DataModel()
                {
                    Id = dataDTO.Id,
                    Temperature = dataDTO.Temperature,
                    Rain = dataDTO.Rain,
                    Humidity = dataDTO.Humidity
                });
            }

            return Ok(listData);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DataModel data)
        {
            DataDTO dataDTO = new()
            {
                Id = data.Id,
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity
            };

            Guid id = dataApplication.Add(dataDTO);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]DataModel data)
        {
            DataDTO dataDTO = new()
            {
                Id = data.Id,
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity
            };

            dataApplication.Change(dataDTO);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            dataApplication.Delete(id);

            return Ok(id);
        }
    }
}

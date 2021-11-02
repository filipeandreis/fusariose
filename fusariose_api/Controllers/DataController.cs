using fusariose_api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using fusariose.Domain.Repository;
using fusariose.Application;
using fusariose.Repository;
using fusariose.Application.DTO;
using Microsoft.Extensions.Configuration;

namespace fusariose_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IDataRepository dataRepository;
        private DataApplication dataApplication;

        public DataController(IConfiguration configuration)
        {
            string strConexao = configuration.GetConnectionString("dbconnection");

            if (String.IsNullOrEmpty(strConexao))
            {
                strConexao = "Server=fanny.db.elephantsql.com;Port=5432;Database=cgaxvztm;User Id=cgaxvztm; Password=ottALDdPtW1HxrMlfH2q1rwzNgMnNAMd;";
            }

            dataRepository = new DataRepository(strConexao);
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
                    Humidity = dataDTO.Humidity,
                    Month = dataDTO.Month
                });
            }

            return Ok(listData);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DataModel data)
        {
            DataDTO dataDTO = new()
            {
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity,
                Month = data.Month
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
                Humidity = data.Humidity,
                Month = data.Month
            };

            dataApplication.Change(dataDTO);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                dataApplication.Delete(id);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

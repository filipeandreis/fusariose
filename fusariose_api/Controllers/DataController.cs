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
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository dataRepository;
        private readonly DataApplication dataApplication;

        public DataController(IConfiguration configuration)
        {
            string strConexao = configuration.GetConnectionString("dbconnection");

            dataRepository = new DataRepository(strConexao);
            dataApplication = new DataApplication(dataRepository);
        }

        [HttpGet]
        [Route("api/[controller]/analyze")]
        public IActionResult AnalyzeData()
        {
            ConvertData();

            var allData = dataApplication.GetAllUnanalyzed();

            List<DataModel> listDataAnalyze = new();

            foreach (var dataDTO in allData)
            {
                if (dataDTO.Temperature < 22 && dataDTO.Humidity >= 70 && dataDTO.Rain > 0)
                {
                    DataModel dataUpdate = new()
                    {
                        Id = dataDTO.Id,
                        Date = dataDTO.Date,
                        Humidity = dataDTO.Humidity,
                        Rain = dataDTO.Rain,
                        Temperature = dataDTO.Temperature,
                        Risk = "true"
                    };

                    Update(dataUpdate);

                    listDataAnalyze.Add(dataUpdate);
                } else
                {
                    DataModel dataUpdate = new()
                    {
                        Id = dataDTO.Id,
                        Date = dataDTO.Date,
                        Humidity = dataDTO.Humidity,
                        Rain = dataDTO.Rain,
                        Temperature = dataDTO.Temperature,
                        Risk = "false"
                    };

                    Update(dataUpdate);
                }
            }

            return Ok(listDataAnalyze);
        }

        [HttpGet]
        [Route("api/[controller]/convert-data")]
        public IActionResult ConvertData()
        {
            dataApplication.ConvertData();

            return Ok();
        }

        [HttpGet]
        [Route("api/[controller]/Get")]
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
                    Date = dataDTO.Date,
                    Risk = dataDTO.Risk
                });
            }

            return Ok(listData);
        }


        [HttpGet]
        [Route("api/[controller]/GetRisk")]
        public IActionResult GetRisk()
        {
            var allData = dataApplication.GetAllWithRisk();

            List<DataModel> listData = new();

            foreach (var dataDTO in allData)
            {
                listData.Add(new DataModel()
                {
                    Id = dataDTO.Id,
                    Temperature = dataDTO.Temperature,
                    Rain = dataDTO.Rain,
                    Humidity = dataDTO.Humidity,
                    Date = dataDTO.Date,
                    Risk = dataDTO.Risk
                });
            }

            return Ok(listData);
        }

        [HttpPost]
        [Route("api/[controller]/GetFilter/{filter}")]
        public IActionResult GetFilter([FromRoute] string filter, [FromBody] YearModel year)
        {
            List<DataDTO> data = new();
            List<MonthDataDTO> monthData = new();

            if (filter.Equals("month"))
            {
                monthData = dataApplication.GetAllMonth();
            }
            else if (filter.Equals("year"))
            {
                data = dataApplication.GetAllYear(Int32.Parse(year.year));
            }
            else
            {
                data = dataApplication.GetAllDay();
            }

            List<DataModel> listData = new();

            foreach (var dataDTO in data)
            {
                listData.Add(new DataModel()
                {
                    Id = dataDTO.Id,
                    Temperature = dataDTO.Temperature,
                    Rain = dataDTO.Rain,
                    Humidity = dataDTO.Humidity,
                    Date = dataDTO.Date,
                    Risk = dataDTO.Risk
                });
            }

            if(monthData.Count > 0)
            {
                return Ok(monthData);
            }
            else
            {
                return Ok(listData);
            }
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Post([FromBody] DataModel data)
        {
            DataDTO dataDTO = new()
            {
                Id = Guid.NewGuid(),
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity,
                Date = data.Date
            };

            Guid id = dataApplication.Add(dataDTO);

            return Ok(id);
        }

        [HttpPut("{id}")]
        [Route("api/[controller]")]
        public IActionResult Put(Guid id, [FromBody]DataModel data)
        {
            DataDTO dataDTO = new()
            {
                Id = data.Id,
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity,
                Date = data.Date,
                Risk = data.Risk
            };

            dataApplication.Change(dataDTO);

            return Ok(id);
        }

        [HttpPut("{id}")]
        [Route("api/[controller]/update")]
        public void Update(DataModel data)
        {
            DataDTO dataDTO = new()
            {
                Id = data.Id,
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity,
                Date = data.Date,
                Risk = data.Risk
            };

            dataApplication.Change(dataDTO);

            return;
        }

        [HttpDelete("{id}")]
        [Route("api/[controller]")]
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

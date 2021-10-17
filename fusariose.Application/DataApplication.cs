using fusariose.Application.Adapter;
using fusariose.Application.DTO;
using fusariose.Domain.Entidades;
using fusariose.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Application
{
    public class DataApplication
    {
        private readonly IDataRepository dataRepository;
        
        public DataApplication(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public DataDTO Get(Guid idData)
        {
            var data = dataRepository.Get(idData);

            return DataAdapter.ToDataDTO(data);
        }

        public List<DataDTO> GetAll()
        {
            List<Data> data = dataRepository.GetAll();

            List<DataDTO> dataDTO = new();

            foreach(var elem in data)
            {
                dataDTO.Add(DataAdapter.ToDataDTO(elem));
            }

            return dataDTO;
        }

        public Guid Add(DataDTO dataDTO)
        {
            Data data = DataAdapter.ToDataDomain(dataDTO);

            dataRepository.Add(data);

            return data.Id;
        }
        public Guid Change(DataDTO dataDTO)
        {
            Data data = DataAdapter.ToDataDomain(dataDTO);

            dataRepository.Change(data);

            return data.Id;
        }

        public void Delete(Guid idData)
        {
            dataRepository.Delete(idData);
        }
    }
}

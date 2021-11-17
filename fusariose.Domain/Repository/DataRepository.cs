using fusariose.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Domain.Repository
{
    public interface IDataRepository
    {
        public List<Data> GetAll();
        public Data Get(Guid idData);
        public List<Data> GetAllUnanalyzed();
        public void ConvertData();
        public void Add(Data data);
        public void Change(Data data);
        public void Delete(Guid idData);
    }
}

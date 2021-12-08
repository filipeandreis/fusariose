using fusariose.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace fusariose.Domain.Repository
{
    public interface IDataRepository
    {
        public List<Data> GetAll();
        public Data Get(Guid idData);
        public List<Data> GetAllUnanalyzed();
        public List<Data> GetAllWithRisk();
        public List<MonthData> GetAllMonth();
        public List<Data> GetAllYear(int year);
        public List<Data> GetAllDay();
        public void ConvertData();
        public void Add(Data data);
        public void Change(Data data);
        public void Delete(Guid idData);
    }
}

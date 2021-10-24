using System;
using System.Collections.Generic;
using fusariose.Domain.Entidades;
using fusariose.Domain.Repository;
using Npgsql;

namespace fusariose.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly string strConexao = "Server=fanny.db.elephantsql.com;Port=5432;Database=cgaxvztm;User Id=cgaxvztm; Password=ottALDdPtW1HxrMlfH2q1rwzNgMnNAMd;";
        public void Add(Data Data)
        {
            using NpgsqlConnection conn = new(strConexao);

            conn.Open();

            NpgsqlCommand query = new()
            {
                Connection = conn,

                CommandText = "INSERT INTO public.data (id, temperature, rain, humidity, month) VALUES (@id, @tempterature, @rain, @humidity, @month);"
            };

            query.Parameters.AddWithValue("id", Data.Id);
            query.Parameters.AddWithValue("tempterature", Data.Temperature);
            query.Parameters.AddWithValue("rain", Data.Rain);
            query.Parameters.AddWithValue("humidity", Data.Humidity);
            query.Parameters.AddWithValue("month", Data.Month);

            query.ExecuteNonQuery();
        }

        public void Change(Data Data)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid idData)
        {
            throw new NotImplementedException();
        }

        public List<Data> GetAll()
        {
            List<Data> listData = new List<Data>();

            using (NpgsqlConnection conn = new(strConexao))
            {
                conn.Open();

                NpgsqlCommand query = new()
                {
                    Connection = conn,

                    CommandText = "SELECT * FROM public.Data;"
                };

                NpgsqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listData.Add(
                        new Data()
                        {
                            Id = Guid.Parse(reader["id"].ToString()),
                            Temperature = Int32.Parse(reader["temperature"].ToString()),
                            Rain = reader["rain"].ToString(),
                            Humidity = reader["humidity"].ToString(),
                            Month = reader["month"].ToString()
                        });
                }
            }
            return listData;
        }

        public Data Get(Guid idData)
        {
            throw new NotImplementedException();
        }
    }
}

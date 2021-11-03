using System;
using System.Collections.Generic;
using fusariose.Domain.Entidades;
using fusariose.Domain.Repository;
using Npgsql;

namespace fusariose.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly string strConexao;

        public DataRepository(string strConexao)
        {
            this.strConexao = strConexao;
        }

        public void Add(Data Data)
        {
            using NpgsqlConnection conn = new(strConexao);

            conn.Open();

            NpgsqlCommand query = new()
            {
                Connection = conn,

                CommandText = "INSERT INTO public.data (id, temperature, rain, humidity, date) VALUES (@id, @tempterature, @rain, @humidity, @date);"
            };

            query.Parameters.AddWithValue("id", Data.Id);
            query.Parameters.AddWithValue("tempterature", Data.Temperature);
            query.Parameters.AddWithValue("rain", Data.Rain);
            query.Parameters.AddWithValue("humidity", Data.Humidity);
            query.Parameters.AddWithValue("date", Data.Date);

            query.ExecuteNonQuery();
        }

        public void Change(Data Data)
        {
            using NpgsqlConnection conn = new(strConexao);

            conn.Open();

            NpgsqlCommand query = new()
            {
                Connection = conn,

                CommandText = "UPDATE data SET temperature = @temperature, rain = @rain, humidity = @humidity, date = @date WHERE id = @id;"
            };

            query.Parameters.AddWithValue("id", Data.Id);
            query.Parameters.AddWithValue("temperature", Data.Temperature);
            query.Parameters.AddWithValue("rain", Data.Rain);
            query.Parameters.AddWithValue("humidity", Data.Humidity);
            query.Parameters.AddWithValue("date", Data.Date);

            query.ExecuteNonQuery();
        }

        public void Delete(Guid idData)
        {
            using NpgsqlConnection conn = new(strConexao);
            conn.Open();
            NpgsqlCommand comando = new();
            comando.Connection = conn;
            comando.CommandText = "DELETE FROM data WHERE id=@id";
            comando.Parameters.AddWithValue("id", idData);
            comando.ExecuteNonQuery();
        }

        public List<Data> GetAll()
        {
            List<Data> listData = new();

            using (NpgsqlConnection conn = new(strConexao))
            {
                conn.Open();

                NpgsqlCommand query = new()
                {
                    Connection = conn,

                    CommandText = "SELECT * FROM data;"
                };

                NpgsqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listData.Add(
                        new Data()
                        {
                            Id = Guid.Parse(reader["id"].ToString()),
                            Temperature = Int32.Parse(reader["temperature"].ToString()),
                            Rain = Convert.ToBoolean(reader["rain"].ToString()),
                            Humidity = Convert.ToBoolean(reader["humidity"].ToString()),
                            Date = Convert.ToDateTime(reader["date"].ToString())
                        }); ;
                }
            }
            return listData;
        }

        public Data Get(Guid idData)
        {
            Data data = null;

            using (NpgsqlConnection con = new NpgsqlConnection())
            {
                con.Open();
                NpgsqlCommand comando = new()
                {
                    Connection = con,

                    CommandText = "SELECT * FROM data where id=@id"
                };
                comando.Parameters.AddWithValue("id", idData);

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    data = new Data()
                    {
                        Id = Guid.Parse(reader["id"].ToString()),
                        Temperature = Int32.Parse(reader["temperature"].ToString()),
                        Rain = Convert.ToBoolean(reader["rain"].ToString()),
                        Humidity = Convert.ToBoolean(reader["humidity"].ToString()),
                        Date = Convert.ToDateTime(reader["date"].ToString())
                    };
                }
            }
            return data;
        }
    }
}

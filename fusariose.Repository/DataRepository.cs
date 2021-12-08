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

                CommandText = "INSERT INTO data (id, temperature, rain, humidity, date, risk) VALUES (@id, @tempterature, @rain, @humidity, @date, @risk);"
            };

            query.Parameters.AddWithValue("id", Data.Id);
            query.Parameters.AddWithValue("tempterature", Data.Temperature);
            query.Parameters.AddWithValue("rain", Data.Rain);
            query.Parameters.AddWithValue("humidity", Data.Humidity);
            query.Parameters.AddWithValue("date", Data.Date);
            query.Parameters.AddWithValue("risk", Data.Risk);

            query.ExecuteNonQuery();
        }

        public void Change(Data Data)
        {
            using NpgsqlConnection conn = new(strConexao);

            conn.Open();

            NpgsqlCommand query = new()
            {
                Connection = conn,

                CommandText = "UPDATE data SET temperature = @temperature, rain = @rain, humidity = @humidity, date = @date, risk = @risk WHERE id = @id;"
            };

            query.Parameters.AddWithValue("id", Data.Id.ToString());
            query.Parameters.AddWithValue("temperature", Data.Temperature);
            query.Parameters.AddWithValue("rain", Data.Rain);
            query.Parameters.AddWithValue("humidity", Data.Humidity);
            query.Parameters.AddWithValue("date", Data.Date);
            query.Parameters.AddWithValue("risk", Convert.ToBoolean(Data.Risk));

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
                            Rain = Int32.Parse(reader["rain"].ToString()),
                            Humidity = Int32.Parse(reader["humidity"].ToString()),
                            Date = Convert.ToDateTime(reader["date"].ToString()),
                            Risk = reader["risk"].ToString()
                        }); ;
                }
            }
            return listData;
        }

        public List<Data> GetAllWithRisk()
        {
            List<Data> listData = new();

            using (NpgsqlConnection conn = new(strConexao))
            {
                conn.Open();

                NpgsqlCommand query = new()
                {
                    Connection = conn,

                    CommandText = "SELECT * FROM data WHERE RISK IS TRUE;"
                };

                NpgsqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listData.Add(
                        new Data()
                        {
                            Id = Guid.Parse(reader["id"].ToString()),
                            Temperature = Int32.Parse(reader["temperature"].ToString()),
                            Rain = Int32.Parse(reader["rain"].ToString()),
                            Humidity = Int32.Parse(reader["humidity"].ToString()),
                            Date = Convert.ToDateTime(reader["date"].ToString()),
                            Risk = reader["risk"].ToString()
                        }); ;
                }
            }
            return listData;
        }

        public List<MonthData> GetAllMonth()
        {
            List<MonthData> listData = new();

            using (NpgsqlConnection conn = new(strConexao))
            {
                conn.Open();

                NpgsqlCommand query = new()
                {
                    Connection = conn,

                    CommandText = "WITH PRAGAS (MES, MEDIA_TEMPERATURA, MEDIA_CHUVA, MEDIA_UMIDADE) AS ( SELECT DISTINCT EXTRACT(MONTH FROM A.DATE) AS MES, (( SELECT SUM(TEMPERATURE) FROM DATA A1 WHERE EXTRACT(MONTH FROM A1.DATE) = EXTRACT(MONTH FROM A.DATE) ) / ( SELECT COUNT(*) FROM DATA A1 WHERE EXTRACT(MONTH FROM A1.DATE) = EXTRACT(MONTH FROM A.DATE) )) AS MEDIA_TEMPERATURA, (( SELECT SUM(RAIN) FROM DATA A1 WHERE EXTRACT(MONTH FROM A1.DATE) = EXTRACT(MONTH FROM A.DATE) ) / ( SELECT COUNT(*) FROM DATA A1 WHERE EXTRACT(MONTH FROM A1.DATE) = EXTRACT(MONTH FROM A.DATE) )) AS MEDIA_CHUVA, (( SELECT SUM(HUMIDITY) FROM DATA A1 WHERE EXTRACT(MONTH FROM A1.DATE) = EXTRACT(MONTH FROM A.DATE) ) / ( SELECT COUNT(*) FROM DATA A1 WHERE EXTRACT(MONTH FROM A1.DATE) = EXTRACT(MONTH FROM A.DATE) )) AS MEDIA_UMIDADE FROM DATA A /*WHERE EXTRACT(MONTH FROM A.DATE) = :MES*/ ) SELECT MES, MEDIA_TEMPERATURA, MEDIA_CHUVA, MEDIA_UMIDADE, CASE WHEN MEDIA_TEMPERATURA < 24 AND MEDIA_CHUVA > 0 AND MEDIA_UMIDADE > 70 THEN TRUE ELSE FALSE END AS RISK FROM PRAGAS;"
                };

                NpgsqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listData.Add(
                        new MonthData()
                        {
                            Month = reader["month"].ToString(),
                            Temperature = Int32.Parse(reader["temperature"].ToString()),
                            Rain = Int32.Parse(reader["rain"].ToString()),
                            Humidity = Int32.Parse(reader["humidity"].ToString()),
                            Risk = reader["risk"].ToString()
                        }); ;
                }
            }
            return listData;
        }

        public List<Data> GetAllDay()
        {
            List<Data> listData = new();

            using (NpgsqlConnection conn = new(strConexao))
            {
                conn.Open();

                NpgsqlCommand query = new()
                {
                    Connection = conn,

                    CommandText = "SELECT * FROM data WHERE RISK IS TRUE;"
                };

                NpgsqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listData.Add(
                        new Data()
                        {
                            Id = Guid.Parse(reader["id"].ToString()),
                            Temperature = Int32.Parse(reader["temperature"].ToString()),
                            Rain = Int32.Parse(reader["rain"].ToString()),
                            Humidity = Int32.Parse(reader["humidity"].ToString()),
                            Date = Convert.ToDateTime(reader["date"].ToString()),
                            Risk = reader["risk"].ToString()
                        }); ;
                }
            }
            return listData;
        }

        public List<Data> GetAllYear(int year)
        {
            List<Data> listData = new();

            using (NpgsqlConnection conn = new(strConexao))
            {
                conn.Open();

                NpgsqlCommand query = new()
                {
                    Connection = conn,

                    CommandText = "WITH PRAGAS (temperature, rain, humidity) AS ( SELECT DISTINCT CAST((( SELECT CAST(SUM(TEMPERATURE) AS NUMERIC(15,2)) FROM DATA A1 WHERE EXTRACT(YEAR FROM A1.DATE) = @year ) / ( SELECT CAST(COUNT(*) AS NUMERIC(15,2)) FROM DATA A1 WHERE EXTRACT(YEAR FROM A1.DATE) = @year )) AS NUMERIC(15,2)) AS temperature, CAST((( SELECT CAST(SUM(RAIN) AS NUMERIC(15,2)) FROM DATA A1 WHERE EXTRACT(YEAR FROM A1.DATE) = @year ) / ( SELECT CAST(COUNT(*) AS NUMERIC(15,2)) FROM DATA A1 WHERE EXTRACT(YEAR FROM A1.DATE) = @year )) AS NUMERIC(15,2)) AS rain, CAST((( SELECT CAST(SUM(HUMIDITY) AS NUMERIC(15,2)) FROM DATA A1 WHERE EXTRACT(YEAR FROM A1.DATE) = @year ) / ( SELECT CAST(COUNT(*) AS NUMERIC(15,2)) FROM DATA A1 WHERE EXTRACT(YEAR FROM A1.DATE) = @year )) AS NUMERIC(15,2)) AS humidity FROM DATA A WHERE EXTRACT(YEAR FROM A.DATE) = @year ) SELECT temperature, rain, humidity, CASE WHEN temperature < 24 AND rain > 0 AND humidity > 70 THEN TRUE ELSE FALSE END AS RISK FROM PRAGAS;"
                };

                query.Parameters.AddWithValue("year", year);

                NpgsqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listData.Add(
                        new Data()
                        {
                            Temperature = Int32.Parse(reader["temperature"].ToString()),
                            Rain = Int32.Parse(reader["rain"].ToString()),
                            Humidity = Int32.Parse(reader["humidity"].ToString()),
                            Risk = reader["risk"].ToString()
                        }); ;
                }
            }
            return listData;
        }

        public List<Data> GetAllUnanalyzed()
        {
            List<Data> listDataUnanalyzed = new();

            using (NpgsqlConnection conn = new(strConexao))
            {
                conn.Open();

                NpgsqlCommand query = new()
                {
                    Connection = conn,

                    CommandText = "SELECT * FROM data WHERE RISK IS NULL;"
                };

                NpgsqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listDataUnanalyzed.Add(
                        new Data()
                        {
                            Id = Guid.Parse(reader["id"].ToString()),
                            Temperature = Int32.Parse(reader["temperature"].ToString()),
                            Rain = Int32.Parse(reader["rain"].ToString()),
                            Humidity = Int32.Parse(reader["humidity"].ToString()),
                            Date = Convert.ToDateTime(reader["date"].ToString()),
                            Risk = reader["risk"].ToString()
                        }); ;
                }
            }
            return listDataUnanalyzed;
        }

        public void ConvertData()
        {
            using NpgsqlConnection conn = new(strConexao);

            conn.Open();

            NpgsqlCommand query = new()
            {
                Connection = conn,

                CommandText = "UPDATE data SET temperature = temperature - 273.15 where temperature >= 273.15;"
            };

            query.ExecuteNonQuery();

            return;
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
                        Rain = Int32.Parse(reader["rain"].ToString()),
                        Humidity = Int32.Parse(reader["humidity"].ToString()),
                        Date = Convert.ToDateTime(reader["date"].ToString()),
                        Risk = reader["risk"].ToString()
                    };
                }
            }
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Roulette.Contracts;
using Roulette.Models;

namespace Roulette.Repositories
{
    public class RouletteRepositorySQL : IRouletteRepository
    {
        private static readonly SqlDbConnection _db = new SqlDbConnection();
        public int AddRoulette(RouletteModel roulette)
        {
            string sql = $"INSERT INTO ROULETTE (ISOPEN)" +
                         $"VALUES ('{ roulette.IsOpen }');" +
                         "SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, _db.connection)
            {
                CommandType = CommandType.Text
            };
            _db.connection.Open();
            try
            {
                int rouletteId = Convert.ToInt32(cmd.ExecuteScalar());

                return rouletteId;
            }
            catch (Exception ex)
            {
                //todo: implementar logs;

                return 0;
            }
            finally
            {
                _db.connection.Close();
            }
        }
        public RouletteModel GetRouletteById(int rouletteId)
        {
            string sql = $"SELECT * FROM ROULETTE WHERE ROULETTEID = { rouletteId }";
            RouletteModel roulette = new RouletteModel();
            SqlCommand cmd = new SqlCommand(sql, _db.connection)
            {
                CommandType = CommandType.Text
            };
            _db.connection.Open();
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    roulette = new RouletteModel
                    {
                        RouletteId = (int)reader["RouletteId"],
                        IsOpen = (bool)reader["IsOpen"]
                    };
                }

                return roulette;
            }
            catch (Exception ex)
            {
                //todo: implementar logs;

                return roulette;
            }
            finally
            {
                _db.connection.Close();
            }
        }
        public bool ChangeStateRoulette(RouletteModel roulette)
        {
            string sql = $"UPDATE ROULETTE SET ISOPEN = '{ roulette.IsOpen }'" +
                         $"WHERE ROULETTEID =  ({ roulette.RouletteId })";
            SqlCommand cmd = new SqlCommand(sql, _db.connection)
            {
                CommandType = CommandType.Text
            };
            _db.connection.Open();
            try
            {
                int rouletteId = cmd.ExecuteNonQuery();

                return rouletteId != 0;
            }
            catch (Exception ex)
            {
                //todo: implementar logs;

                return false;
            }
            finally
            {
                _db.connection.Close();
            }
        }
        public List<RouletteModel> GetAllRoulettes()
        {
            string sql = "SELECT * FROM ROULETTE";
            List<RouletteModel> roulettes = new List<RouletteModel>();
            SqlCommand cmd = new SqlCommand(sql, _db.connection)
            {
                CommandType = CommandType.Text
            };
            _db.connection.Open();
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    roulettes.Add(new RouletteModel
                    {
                        RouletteId = (int)reader["RouletteId"],
                        IsOpen = (bool)reader["IsOpen"]
                    });
                }

                return roulettes;
            }
            catch (Exception ex)
            {
                //todo: implementar logs;

                return null;
            }
            finally
            {
                _db.connection.Close();
            }
        }
    }
}

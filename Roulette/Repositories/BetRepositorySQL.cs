using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Roulette.Contracts;
using Roulette.Models;

namespace Roulette.Repositories
{
    public class BetRepositorySQL : IBetRepository
    {
        private static readonly SqlDbConnection _db = new SqlDbConnection();
        public int AddBet(BetModel bet)
        {
            string sql = $"INSERT INTO BET (ROULETTEID, USERID, ISACTIVE, NUMBER, COLOR, MONEY)" +
                         $"VALUES ({bet.RouletteId},{bet.UserId}, '{bet.IsActive}', {bet.Number},'{bet.Color}',{bet.Money});" +
                         "SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, _db.connection)
            {
                CommandType = CommandType.Text
            };
            _db.connection.Open();
            try
            {
                int betId = Convert.ToInt32(cmd.ExecuteScalar());
                return betId;
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
        public List<BetModel> GetActiveBets(int rouletteId)
        {
            string sql = $"SELECT * FROM BET WHERE ROULETTEID = {rouletteId} AND ISACTIVE = 'true'";
            List<BetModel> bets = new List<BetModel>();
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
                    bets.Add(new BetModel
                    {
                        BetId = (int)reader["BetId"],
                        UserId = (int)reader["UserId"],
                        IsActive = (bool)reader["IsActive"],
                        RouletteId = (int)reader["RouletteId"],
                        Number = (int)reader["Number"],
                        Color = (string)reader["Color"],
                        Money = Convert.ToDouble(reader["Money"])
                    });
                }

                return bets;
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
        public void CloseBets(int rouletteId)
        {
            string sql = $"UPDATE BET SET ISACTIVE = 'false'" +
                         $"WHERE ROULETTEID =  ({ rouletteId })";
            SqlCommand cmd = new SqlCommand(sql, _db.connection)
            {
                CommandType = CommandType.Text
            };
            _db.connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //todo: implementar logs;
            }
            finally
            {
                _db.connection.Close();
            }
        }
    }
}

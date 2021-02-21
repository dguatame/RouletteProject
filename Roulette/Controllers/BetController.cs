using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Roulette.Models;
using Roulette.Repositories;
namespace Roulette.Controllers
{
    [Route("api/[controller]")]
    public class BetController : Controller
    {
        // GET: api/Bet/CloseBets/{rouletteId}
        [HttpGet("CloseBets/{rouletteId}")]
        public IEnumerable<string> Get(int rouletteId)
        {
            RouletteRepositorySQL rouletteRepository = new RouletteRepositorySQL();
            RouletteModel roulette = rouletteRepository.GetRouletteById(rouletteId: rouletteId);
            if (roulette.IsValidToClose())
            {
                roulette.IsOpen = false;
                rouletteRepository.ChangeStateRoulette(roulette: roulette);
            }
            else
            {
                return new string[] { $"La ruleta {rouletteId} no se puede cerrar."};
            }
            BetRepositorySQL betRepository = new BetRepositorySQL();
            List<BetModel> bets = betRepository.GetActiveBets(rouletteId: rouletteId);
            if (!bets.Any())
                return new string[] { $"La ruleta {rouletteId} se cerró, pero no tenia apuestas." };
            betRepository.CloseBets(rouletteId: rouletteId);

            return GetBetsResults(bets: bets);
        }
        // POST api/Bet/AddBet
        //Example body
        /*
        {
            "betId": 1,
            "rouletteId": 1,
            "number": 37,
            "color": "Negro",
            "money": 5200
        }
        */
        [HttpPost("AddBet")]
        public string Post([FromBody] BetModel bet)
        {
            var userId = Request.Headers["UserId"];
            bet.UserId = Convert.ToInt32(userId);
            if (bet.IsValidToBet())
            {
                BetRepositorySQL betRepository = new BetRepositorySQL();
                bet.IsActive = true;
                bet.BetId = betRepository.AddBet(bet: bet);
                if (bet.BetId != 0)
                    return $"La apuesta se ha recibido correctamente en la ruleta {bet.RouletteId}";
                else
                    return $"La apuesta en la ruleta {bet.RouletteId} no se ha podido realizar";
            }
            else
            {
                return "La apuesta no se ha podido realizar, por favor verifícala";
            }
        }
        private IEnumerable<string> GetBetsResults(List<BetModel> bets)
        {
            Random random = new Random();
            int winnerNumber = random.Next(0, 36);
            string winnerColor = (winnerNumber % 2 == 0) ? "ROJO" : "NEGRO";
            List<string> results = new List<string>();
            foreach (BetModel bet in bets)
            {
                double earnedMoney = 0;
                if (bet.Number == winnerNumber)
                    earnedMoney += bet.Money * 5;
                if (bet.Color.ToUpper() == winnerColor)
                    earnedMoney += bet.Money * 1.8;
                if (bet.Money == (bet.Money + earnedMoney))
                    results.Add($"La apuesta {bet.BetId} del usuario {bet.UserId} se ha perdido");
                else
                    results.Add($"La apuesta {bet.BetId} del usuario {bet.UserId} ha ganado {earnedMoney} para un total de { bet.Money + earnedMoney }");
            }

            return results;
        }
    }
}

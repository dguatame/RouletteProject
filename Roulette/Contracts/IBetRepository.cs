using System.Collections.Generic;
using Roulette.Models;

namespace Roulette.Contracts
{
    public interface IBetRepository
    {
        bool AddBet(BetModel bet);
        List<BetModel> GetAllBets(int rouletteId);
    }
}

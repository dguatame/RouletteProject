using System.Collections.Generic;
using Roulette.Models;
namespace Roulette.Contracts
{
    public interface IBetRepository
    {
        int AddBet(BetModel bet);
        List<BetModel> GetActiveBets(int rouletteId);
        void CloseBets(int rouletteId);
    }
}

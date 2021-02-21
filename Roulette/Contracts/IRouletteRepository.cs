using System.Collections.Generic;
using Roulette.Models;
namespace Roulette.Contracts
{
    public interface IRouletteRepository
    {
        int AddRoulette(RouletteModel roulette);
        RouletteModel GetRouletteById(int rouletteId);
        bool ChangeStateRoulette(RouletteModel roulette);
        List<RouletteModel> GetAllRoulettes();
    }
}

using System.Collections.Generic;
using Roulette.Models;
namespace Roulette.Contracts
{
    public interface IRouletteRepository
    {
        int AddRoulette(RouletteModel roulette);
        bool ChangeStateRoulette(RouletteModel roulette);
        List<RouletteModel> GetAllRoulettes();
    }
}

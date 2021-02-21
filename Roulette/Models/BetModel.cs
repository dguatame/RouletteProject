using Roulette.Repositories;
namespace Roulette.Models
{
    public class BetModel
    {
        public int BetId { get; set; }
        public int RouletteId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public double Money { get; set; }
        public bool IsValidToBet()
        {
            if (RouletteId == 0)
                return false;
            RouletteRepositorySQL rouletteRepository = new RouletteRepositorySQL();
            RouletteModel roulette = rouletteRepository.GetRouletteById(rouletteId: RouletteId);
            if (roulette.RouletteId == 0)
                return false;
            if (!roulette.IsOpen)
                return false;
            if (UserId == 0)
                return false;
            if (Number < 0 || Number > 36)
                return false;
            if (Color.ToUpper() != "NEGRO" && Color.ToUpper() != "ROJO")
                return false;
            if (Money < 0 || Money > 10000)
                return false;

            return true;
        }
    }
}

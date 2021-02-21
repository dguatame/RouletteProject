using System;
namespace Roulette.Models
{
    public class BetModel
    {
        public int BetId { get; set; }
        public int RouletteId { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public decimal Money { get; set; }
    }
}

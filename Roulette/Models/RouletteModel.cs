namespace Roulette.Models
{
    public class RouletteModel
    {
        public int RouletteId { get; set; }
        public bool IsOpen { get; set; }
        public bool IsValidToOpen()
        {
            if (IsOpen)
                return false;
            if (RouletteId == 0)
                return false;

            return true;
        }
        public bool IsValidToClose()
        {
            if (!IsOpen)
                return false;
            if (RouletteId == 0)
                return false;

            return true;
        }
    }
}

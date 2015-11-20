
namespace BoxUnlocker.Models
{
    public class MumGame
    {
        public MumGame()
            : this(0, 0, 0)
        {
        }
        public MumGame(int iNumberFrom, int iNumberTo, int iHintCount)
        {
            this.NumberFrom = iNumberFrom;
            this.NumberTo = iNumberTo;
            this.HintCount = iHintCount;
        }
        public int NumberFrom { get; set; }
        public int NumberTo { get; set; }
        public int HintCount { get; set; }
    }
}

namespace Common.Entities
{
    public class BowlingRound
    {
        public int FirstRoll { get; set; }
        public int SecondRoll { get; set; }
        public int? ThirdRoll { get; set; }
        public int? RoundScore { get; set; }

        public bool IsStrike()
        {
            return FirstRoll == 10;
        }

    }
}

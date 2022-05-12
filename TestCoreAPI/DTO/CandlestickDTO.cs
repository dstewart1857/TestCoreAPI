namespace TestCoreAPI.DTO
{
    public class CandlestickDTO
    {
        public CandlestickDTO(string title, float max, float quartile3, float quartile1, float min)
        {
            Title = title;
            Max = max;
            Quartile3 = quartile3;
            Quartile1 = quartile1;
            Min = min;
        }

        public String Title { get; set; }
        public float Max { get; set; }
        public float Quartile3 { get; set; }
        public float Quartile1 { get; set; }
        public float Min { get; set; }
    }
}

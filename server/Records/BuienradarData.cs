namespace WAMServer.Records
{
    public record BuienradarData
    {
        public int MinTemperatureAvg { get; init; }
        public int MaxTemperatureAvg { get; init; }
        public int WindSpeed { get; init; }
        public int MMRain { get; init; }
    }
}
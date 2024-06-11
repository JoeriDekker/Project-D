namespace WAMServer.Records.Bodies
{
    public record WaterLevelSettingsPatchBodyDecimal
    {
        public decimal? PoleHeight { get; init; }
        public decimal? IdealHeight { get; init; }
    }

        public record WaterLevelSettingsPatchBody
    {
        public string? PoleHeight { get; init; }
        public string? IdealHeight { get; init; }
    }
}
namespace WAMServer.Records.Bodies
{
    public record WaterLevelSettingsPatchBody
    {
        public decimal? PoleHeight { get; init; }
        public decimal? IdealHeight { get; init; }
    }
}
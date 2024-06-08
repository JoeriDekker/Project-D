namespace WAMServer.Records
{
    /// <summary>
    /// Record for storing Buienradar data.
    /// </summary>
    /// <param name="Temperature">The temperature.</param>
    /// <param name="WindSpeed">The wind speed.</param>
    /// <param name="MMRain">The amount of rain in mm.</param>
    /// <param name="SunChance">The chance of sun outed in percentage. For example, 50% is 50</param>
    public record BuienradarData
    {
        public int Temperature { get; init; }
        public int WindSpeed { get; init; }
        public int MMRain { get; init; }
        public int SunChance { get; init; }
        public int Humidity { get; init; }
    }
}
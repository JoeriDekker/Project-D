namespace WAMServer.Records
{
    /// <summary>
    /// Record for storing weather forecast data.
    /// </summary>
    /// <param name="id">The ID of the forecast.</param>
    /// <param name="day">The day of the forecast.</param>
    /// <param name="rainChance">The chance of rain.</param>
    /// <param name="sunChance">The chance of sun.</param>
    /// <param name="wind">The wind speed.</param>
    /// <param name="mmRainMin">The minimum amount of rain in mm.</param>
    /// <param name="mmRainMax">The maximum amount of rain in mm.</param>
    public record WeatherForecast
    (
        string Id,
        DateTime Day,
        int RainChance,
        int SunChance,
        int Wind,
        double MmRainMin,
        double MmRainMax
    );
}
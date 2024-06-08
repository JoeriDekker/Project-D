namespace WAMServer.Records
{
    /// <summary>
    /// Record for storing weather station data.
    /// </summary>
    /// <param name="StationId">The ID of the station.</param>
    /// <param name="StationName">The name of the station.</param>
    /// <param name="Lat">The latitude of the station.</param>
    /// <param name="Lon">The longitude of the station.</param>
    /// <param name="Regio">The region of the station.</param>
    /// <param name="Timestamp">The timestamp of the measurement.</param>
    /// <param name="WindDirection">The wind direction.</param>
    /// <param name="AirPressure">The air pressure.</param>
    /// <param name="Temperature">The temperature.</param>
    /// <param name="GroundTemperature">The ground temperature.</param>
    /// <param name="FeelTemperature">The feel temperature.</param>
    /// <param name="WindSpeed">The wind speed.</param>
    /// <param name="Humidity">The humidity.</param>
    /// <param name="SunPower">The sun power.</param>
    /// <param name="RainFallLast24Hour">The rainfall in the last 24 hours.</param>
    /// <param name="RainFallLastHour">The rainfall in the last hour.</param>
    /// <param name="WindDirectionDegrees">The wind direction in degrees.</param>
    public record WeatherMeasurement(
    int StationId,
    string StationName,
    double Lat,
    double Lon,
    string Regio,
    DateTime Timestamp,
    string WindDirection,
    double AirPressure,
    double Temperature,
    double GroundTemperature,
    double FeelTemperature,
    double WindSpeed,
    double Humidity
);
}

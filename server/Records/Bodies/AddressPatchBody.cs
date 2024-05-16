namespace WAMServer.Records.Bodies
{
    /// <summary>
    /// Address patch body
    /// </summary>
    public record AddressPatchBody
    {
        public string? Street { get; init; }
        public string? HouseNumber { get; init; }
        public string? City { get; init; }
        public string? Zip { get; init; }
        public string? Password { get; init; }
    }
}
namespace WAMServer.Records.Bodies
{
    /// <summary>
    /// Address patch body
    /// </summary>
    public record AddressPatchBody
    {
        public string Street = null!;
        public string HouseNumber = null!;
        public string City = null!;
        public string Zip = null!;
        public string Password = null!;
    }
}
using System.Text.Json.Serialization;

namespace BlatchAPI.Entities
{
    public class Address
    {
        public Guid ID { get; set; }
        [JsonPropertyName("streetNumber")]
        public string StreetNumber { get; set; }
        [JsonPropertyName("streetName")]
        public string StreetName { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("stateOrCounty")]
        public string StateOrCounty { get; set; }
        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
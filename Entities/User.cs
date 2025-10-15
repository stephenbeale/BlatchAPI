using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace BlatchAPI.Entities
{
    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("address")]
        public Address Address { get; set; }
        [JsonPropertyName("age")]
        public int? Age { get; set; }
        [JsonPropertyName("gender")]
        public string? Gender { get; set; }
        [JsonPropertyName("company")]
        public string? Company { get; set; }
        [JsonPropertyName("department")]
        public string? Department { get; set; }
        [JsonPropertyName("headshotImage")]
        public string HeadshotImage { get; set; }
        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }
        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }
        [JsonPropertyName("skills")]
        public IEnumerable<string>? Skills { get; set; }
        [JsonPropertyName("colleagues")]
        public IEnumerable<string>? Colleagues { get; set; }
        [JsonPropertyName("employmentStart")]
        public DateTimeOffset? EmploymentStart { get; set; }
        [JsonPropertyName("employmentEnd")]
        public DateTimeOffset? EmploymentEnd { get; set; }
        [JsonPropertyName("fullName")]
        public string? FullName { get; set; } = string.Empty;
    }
}
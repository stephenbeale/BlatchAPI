using System.Reflection.Metadata;

namespace BlatchAPI.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Address Address { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Company { get; set; }
        public string Department { get; set; }
        public Blob HeadshotImage { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public IEnumerable<string>? Skills { get; set; }
        public IEnumerable<string>? Colleagues { get; set; }
        public DateTimeOffset? EmploymentStart { get; set; }
        public DateTimeOffset? EmploymentEnd { get; set; }
        public string? FullName { get; set; } = string.Empty;
    }
}
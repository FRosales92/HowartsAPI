using System.Text.Json.Serialization;

#nullable disable

namespace HowartsAPI.Models
{
    public partial class Candidate
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
        public int HouseId { get; set; }
        [JsonIgnore]
        public virtual House House { get; set; }
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace HowartsAPI.Models
{
    public partial class House
    {
        public House()
        {
            Candidates = new HashSet<Candidate>();
        }

        public int HouseId { get; set; }
        public string NameHouse { get; set; }
        [JsonIgnore]
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}

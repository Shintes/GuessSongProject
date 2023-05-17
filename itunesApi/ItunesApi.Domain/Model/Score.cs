using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ItunesApi.Domain.Model
{
    public record Score
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }

    }
}

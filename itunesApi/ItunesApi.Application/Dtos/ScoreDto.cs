using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ItunesApi.Application.Dtos
{
    public record ScoreDto
    {
      public int Id { get; set; }
      public int Points { get; set; }
      public DateTime Date { get; set; }
      public int AccountId { get; set; }
    }
}

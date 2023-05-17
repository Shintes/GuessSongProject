using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ItunesApi.Domain.Model
{
    public class ScoreResult
    {
        public int Score { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
    }
}

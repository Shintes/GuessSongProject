﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ItunesApi.Domain.Model
{
    public record Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public List<Score> ScoreList { get; set; }
    }
}

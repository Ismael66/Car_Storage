using System;
using Newtonsoft.Json;

namespace Car_Storage
{
    public class Carro
    {
        [JsonProperty("marca")]
        public string? marca { get; set; }
        [JsonProperty("modelo")]
        public string? modelo { get; set; }
        [JsonProperty("ano")]
        public int ano { get; set; }
        [JsonProperty("placa")]
        public string? placa { get; set; }
    }
}
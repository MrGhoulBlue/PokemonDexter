using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public partial class PokemonApiModel
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public PokemonModel[] Results { get; set; }
    }

   
}

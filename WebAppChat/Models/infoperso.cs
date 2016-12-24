using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppChat.Models
{
    public class infoperso
    {
        public int Id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string age { get; set; }
        [JsonProperty(PropertyName = "adresse")]

        public string adresse { get; set; }
    }
}
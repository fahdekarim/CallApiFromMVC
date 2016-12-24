using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppChat.Models
{
    public class IOTSigfoxData
    {
        [JsonProperty(PropertyName = "data")]
        public List<IOTSigfox> data { get; set; }
       
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppInfoPersoMVC.Models
{
    public class IOTSigfoxObject
    {
        [JsonProperty(PropertyName = "data")]
        IOTSigfoxData data { get; set; }

    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppInfoPersoMVC.Models
{
    public class IOTSigfoxData
    {
        [JsonProperty(PropertyName = "data")]
        List<IOTSigfox> data { get; set; }
       
    }
}
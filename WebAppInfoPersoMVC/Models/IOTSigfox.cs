using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppInfoPersoMVC.Models
{
    public class IOTSigfox
    {
        [JsonProperty(PropertyName = "device")]
        public string device { get; set; }          //" : "17AA23",
        [JsonProperty(PropertyName = "time")]
        public string time { get; set; }            //" : 1480870210,
        [JsonProperty(PropertyName = "data")]
        public string data { get; set; }            //" : "17",
        [JsonProperty(PropertyName = "snr")]
        public string snr { get; set; }             //" : "15.04",
        [JsonProperty(PropertyName = "linkQuality")]
        public string linkQuality { get; set; }     //" : "AVERAGE"
    }
}
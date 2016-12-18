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
        string device { get; set; }          //" : "17AA23",
        [JsonProperty(PropertyName = "time")]
        string time { get; set; }            //" : 1480870210,
        [JsonProperty(PropertyName = "data")]
        string data { get; set; }            //" : "17",
        [JsonProperty(PropertyName = "snr")]
        string snr { get; set; }             //" : "15.04",
        [JsonProperty(PropertyName = "linkQuality")]
        string linkQuality { get; set; }     //" : "AVERAGE"
    }
}
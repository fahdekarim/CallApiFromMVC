using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppChat.Models
{
    public class IOTSigfox
    {
        private string lingQuality;

        public IOTSigfox(string device, string time, string data, string snr, string lingQuality)
        {
            this.device = device;
            this.time = time;
            this.data = data;
            this.snr = snr;
            this.linkQuality = lingQuality;
        }

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
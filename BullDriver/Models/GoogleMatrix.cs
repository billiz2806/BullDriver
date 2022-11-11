using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BullDriver.Models
{
    public class GoogleMatrix
    {
        [JsonProperty(PropertyName = "destination_addresses")]
        public string[] DestinationAddresses { get; set; }

        [JsonProperty(PropertyName = "origin_addresses")]
        public string[] OriginAddresses { get; set; }
        public Row[] Rows { get; set; }

        public class Row
        {
            public Element[] Elements { get; set; }
        }
        public class Element
        {    
            public Data Distance { get; set; }
            public Data Duration { get; set; }
            public string Status { get; set; }
        }
        public class Data
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
        public string Status { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalanderServer
{
    class ToDoItem
    {
        //[JsonProperty("id")]
        //public string Id { get; set; }
        [JsonProperty("description")]
        public string Desription { get; set; }
        [JsonProperty("isComplete")]
        public bool isComplete { get; set; }
    }
}

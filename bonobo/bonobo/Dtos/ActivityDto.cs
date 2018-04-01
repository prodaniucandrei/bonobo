using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Dtos
{
    public class ActivityDto
    {
        [JsonProperty("activityHostId")]
        public string ActivityHostId { get; set; }
        [JsonProperty("activityId")]
        public string ActivityId { get; set; }
        [JsonProperty("activityName")]
        public string ActivityName { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }
        [JsonProperty("noPlaces")]
        public int NoPlaces { get; set; }
        [JsonProperty("where")]
        public string Where { get; set; }
        [JsonProperty("when")]
        public DateTime When { get; set; }
        [JsonProperty("joinedUsersIds")]
        public List<string> JoinedUsersIds { get; set; }
        [JsonProperty("isHost")]
        public bool IsHost { get; set; }
        [JsonProperty("isJoined")]
        public bool IsJoined { get; set; }
    }
}

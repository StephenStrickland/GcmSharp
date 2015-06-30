using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public class GcmNotification
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("sound", NullValueHandling = NullValueHandling.Ignore)]
        public string Sound { get; set; }

        [JsonProperty("badge", NullValueHandling = NullValueHandling.Ignore)]
        public int? Badge { get; set; }

        [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
        public string Tag { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("click_action", NullValueHandling = NullValueHandling.Ignore)]
        public string ClickAction { get; set; }

        [JsonProperty("body_loc_key", NullValueHandling = NullValueHandling.Ignore)]
        public string BodyLocKey { get; set; }

        [JsonProperty("body_loc_args", NullValueHandling = NullValueHandling.Ignore)]
        public string BodyLocArgs { get; set; }

        [JsonProperty("title_loc_args", NullValueHandling = NullValueHandling.Ignore)]
        public string TitleLocArgs { get; set; }

        [JsonProperty("title_loc_key", NullValueHandling = NullValueHandling.Ignore)]
        public string TitleLocKey { get; set; }
    }
}

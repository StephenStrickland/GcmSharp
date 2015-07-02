using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcmSharp
{
    public class GcmDeviceGroupRequest
    {
        [JsonProperty("operation", NullValueHandling = NullValueHandling.Ignore)]
        public string Operation { get; set; }

        [JsonProperty("notification_key_name", NullValueHandling = NullValueHandling.Ignore)]
        public string NotificationKeyName { get; set; }

        [JsonProperty("notification_key", NullValueHandling = NullValueHandling.Ignore)]
        public string NotificationKey { get; set; }

        [JsonProperty("registration_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RegistrationIds { get; set; }

    }

    //public class GcmDeviceGroupRespsonse
    //{

    //}

    public class DeviceGroupOptionsConstants
    {
        public const string Create = "create";
        public const string Add = "add";
        public const string Remove = "remove";
    }
}

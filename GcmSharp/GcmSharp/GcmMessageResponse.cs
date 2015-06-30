using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public class GcmMessageResponse : IResponse
    {
        [JsonProperty("multicast_id", NullValueHandling = NullValueHandling.Ignore)]
        public long MultiCastId { get; set; }

        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public int MessagesSucceededCount { get; set; }

        [JsonProperty("failure", NullValueHandling = NullValueHandling.Ignore)]
        public int MessagesFailedCount { get; set; }

        [JsonProperty("canonical_ids", NullValueHandling = NullValueHandling.Ignore)]
        public int CoanonicalIds { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<MessageStatus> Results { get; set; }

        public GcmMessage Message { get; set; }
        public HttpWebResponse HttpWebResponse { get; set; }
        public WebRequest WebRequest { get; set; }
        public ENUM_GCM_MESSAGE_RESPONSE_TYPES ResponseStatus {get;set;}
        public bool Success { get { return (ResponseStatus == ENUM_GCM_MESSAGE_RESPONSE_TYPES.SUCCESS && MessagesFailedCount == 0); } }
    }
    public enum ENUM_GCM_MESSAGE_RESPONSE_TYPES
    {
        SUCCESS = 0,
        MISSING_REGISTRATION_TOKEN = 1,
        INVALID_REGISTRATION_TOKEN=2,
        UNREGISTED_DEVICE=3,
        INVALID_PACKAGE_NAME = 4,
        AUTHENTICATION_ERROR = 401,
        MISMATHED_SENDER = 5,
        INVALID_JSON = 400,
        MESSAGE_TOO_BIG = 6, 
        INVALID_DATA_KEY = 7,
        INVALID_TIME_TO_LIVE = 8,
        BAD_ACK_MESSAGE = 100,
        TIMEOUT = 9,
        INTERNAL_SERVER_ERROR = 500,
        DEVICE_MESSAGE_RATE_EXCEEDED = 10,
        TOPICS_MESSAGE_RATE_EXCEEDED = 11,
        //CONNECTION_DRAINING = 11,
        TOPIC_TOO_MANY_SUBSCRIBERS = 50,
        TOPIC_INVALID_PARAMETERS = 51
    }

    public class MessageStatus
    {

        [JsonProperty("message_id", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageId { get; set; }

        [JsonProperty("registration_id", NullValueHandling = NullValueHandling.Ignore)]
        public string RegistrationId { get; set; }

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        public ENUM_GCM_MESSAGE_RESPONSE_TYPES Status { get; set; }

    }
}

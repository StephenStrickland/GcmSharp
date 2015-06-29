using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public class GcmMessageResponse
    {
        public int MultiCastId { get; set; }
        public int GcmSuccess { get; set; }
        public GcmMessage Message { get; set; }
    }
    public enum ENUM_GCM_RESPONSE_TYPES
    {
        NONE = 0,
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
}

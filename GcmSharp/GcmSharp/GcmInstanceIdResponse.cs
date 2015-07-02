using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GcmSharp
{
    public class GcmInstanceIdResponse : IResponse
    {
        [JsonProperty("application", NullValueHandling = NullValueHandling.Ignore)]
        public string Application { get; set; }

        [JsonProperty("authorizedEntity", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorizedEntity { get; set; }

        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; set; }

        [JsonProperty("attestStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string AttestStatus { get; set; }

        [JsonProperty("appSigner", NullValueHandling = NullValueHandling.Ignore)]
        public string AppSigner { get; set; }

        [JsonProperty("connectionType", NullValueHandling = NullValueHandling.Ignore)]
        public string ConnectionType { get; set; }

        [JsonProperty("connectionDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ConnectDateTime { get; set; }
        public INSTANCE_ID_RESPONSE_STATUS ResponseStatus { get; set; }
        public bool Success { get { return ResponseStatus == INSTANCE_ID_RESPONSE_STATUS.SUCCESS; } }
        public HttpWebResponse HttpWebResponse { get; set; }
        public WebRequest WebRequest { get; set; }

        //public object WebResponseData { get; set; }
    }

    public enum INSTANCE_ID_RESPONSE_STATUS
    {
        SUCCESS = 200,
        FORBIDDEN = 403,
        UNAUTHORIZED = 401,
        BAD_REQUEST = 400,
        SERVICE_UNAVAILABLE = 503
    }

    public class GcmInstanctIdRequest
    {
        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public bool Details { get; set; }

        [JsonProperty("instance_id", NullValueHandling = NullValueHandling.Ignore)]
        public string InstanceId { get; set; }
    }

//    {
//  "application":"com.iid.example",
//  "authorizedEntity":"123456782354",
//  "platform":"IOS",
//  "attestStatus":"ROOTED",
//  "appSigner":"1a2bc3d4e5",
//  "connectionType":"WIFI",
//  "connectDate":"2015-05-12
//}


}

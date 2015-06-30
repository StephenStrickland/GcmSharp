using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public class GcmInstanceIdResponse : IResponse
    {
        [JsonProperty("application", NullValueHandling = NullValueHandling.Ignore)]
        public string Application { get; set; }

        [JsonProperty("authorized_entity", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorizedEntity { get; set; }

        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; set; }

        [JsonProperty("attest_status", NullValueHandling = NullValueHandling.Ignore)]
        public string AttestStatus { get; set; }

        [JsonProperty("app_signer", NullValueHandling = NullValueHandling.Ignore)]
        public string AppSigner { get; set; }

        [JsonProperty("connection_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ConnectionType { get; set; }

        [JsonProperty("connection_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ConnectDateTime { get; set; }
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

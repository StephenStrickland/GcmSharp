using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public class GcmInstanceIdRequest
    {
        public string Application { get; set; }
        public string AuthorizedEntity { get; set; }
        public string Platform { get; set; }
        public string AttestStatus { get; set; }
        public string AppSigner { get; set; }
        public string ConnectionType { get; set; }
        public DateTime ConnectDateTime { get; set; }
        public INSTANCE_ID_RESPONSE_STATUS ResponseStatus { get; set; }
        public bool Success { get { return ResponseStatus == INSTANCE_ID_RESPONSE_STATUS.SUCCESS; } }
        public HttpWebResponse WebResponse { get; set; }
        public object WebResponseData { get; set; }
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

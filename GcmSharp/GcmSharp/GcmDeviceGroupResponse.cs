using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public class GcmDeviceGroupResponse : IResponse
    {
        public GcmDeviceGroupRequest Request { get; set; }
        public HttpWebResponse HttpWebResponse { get; set; }
        public WebRequest WebRequest { get; set; }
    }
}

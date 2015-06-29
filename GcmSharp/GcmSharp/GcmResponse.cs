using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public class GcmResponse
    {
        public ENUM_GCM_RESPONSE_TYPES ResponseError { get; set; }
        public bool Success { get; set; }
    }
}

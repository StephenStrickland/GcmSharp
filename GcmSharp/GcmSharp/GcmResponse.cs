using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcmSharp
{
    public class GcmResponse
    {
        public ENUM_GCM_MESSAGE_RESPONSE_TYPES ResponseError { get; set; }
        public bool Success { get; set; }
    }
}

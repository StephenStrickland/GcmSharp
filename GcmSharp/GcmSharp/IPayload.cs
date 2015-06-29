using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    class IPayload
    {
        IGcmNotification Notification { get; set; }
        IGcmData Data { get; set; }

    }
}

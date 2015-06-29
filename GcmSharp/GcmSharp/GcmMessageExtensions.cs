using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public static class GcmMessageExtensions
    {
        public static GcmMessage WithData(this GcmMessage msg, object data)
        {
            msg.Data = data;
            return msg;
        }

        public static GcmMessage To(this GcmMessage msg, string to)
        {
            msg.To = to;
            return msg;
        }

        public static GcmMessage WithNotification(this GcmMessage msg, GcmNotification not)
        {
            msg.Notification = not;
            return msg;
        }

        public static GcmMessage WithRegistrationIds(this GcmMessage msg, List<string> ids)
        {
            if (msg.RegistrationIds == null)
                msg.RegistrationIds = new List<string>();
            msg.RegistrationIds.AddRange(ids);
            return msg;
        }

        public static GcmMessage WithRegistrationId(this GcmMessage msg, string id)
        {
            if (msg.RegistrationIds == null)
                msg.RegistrationIds = new List<string>();
            msg.RegistrationIds.Add(id);
            return msg;
        }

        public static GcmMessage WithMessageId(this GcmMessage msg, string id)
        {
            msg.MessageId = id;
            return msg;
        }

        public static GcmMessage WithCollapseKey(this GcmMessage msg, string key)
        {
            msg.CollapseKey = key;
            return msg;
        }

        public static GcmMessage WithPriority(this GcmMessage msg, int priority)
        {
            msg.Priority = priority;
            return msg;
        }

        public static GcmMessage WithContentAvailable(this GcmMessage msg, bool contentAvailable)
        {
            msg.ContentAvailable = contentAvailable;
            return msg;
        }

        public static GcmMessage WithDelayWhileIdle(this GcmMessage msg, bool delayWhileIdle)
        {
            msg.DelayWhileIdle = delayWhileIdle;
            return msg;
        }

        public static GcmMessage WithTimeToLive(this GcmMessage msg, int timeToLive)
        {
            msg.TimeToLive = timeToLive;
            return msg;
        }

        public static GcmMessage WithDeliveryReceiptRequested(this GcmMessage msg, bool deliveryReceiptRequested)
        {
            msg.DeliveryReceiptRequested = deliveryReceiptRequested;
            return msg;
        }

        public static GcmMessage WithRestrictedPackageName(this GcmMessage msg, string packageName)
        {
            msg.RestrictedPackageName = packageName;
            return msg;
        }

        public static GcmMessage IsDryRun(this GcmMessage msg, bool isDryRun)
        {
            msg.DryRun = isDryRun;
            return msg;
        }

        


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public static class GcmNotificationExtensions
    {
        public static GcmNotification WithTitle(this GcmNotification not, string title)
        {
            not.Title = title;
            return not;
        }

        public static GcmNotification WithBody(this GcmNotification not, string body)
        {
            not.Body = body;
            return not;
        }

        public static GcmNotification WithIcon(this GcmNotification not, string icon)
        {
            not.Icon = icon;
            return not;
        }

        public static GcmNotification WithSound(this GcmNotification not, string sound)
        {
            not.Sound = sound;
            return not;
        }

        public static GcmNotification WithBadge(this GcmNotification not, int badge)
        {
            not.Badge = badge;
            return not;
        }

        public static GcmNotification WithTag(this GcmNotification not, string tag)
        {
            not.Tag = tag;
            return not;
        }

        public static GcmNotification WithColor(this GcmNotification not, string color)
        {
            not.Color = color;
            return not;
        }

        public static GcmNotification WithClickAction(this GcmNotification not, string clickAction)
        {
            not.ClickAction = clickAction;
            return not;
        }

        public static GcmNotification WithBodyLocKey(this GcmNotification not, string bodyLocKey)
        {
            not.BodyLocKey = bodyLocKey;
            return not;
        }

        public static GcmNotification WithBodyLocArgs(this GcmNotification not, string bodyLocArgs)
        {
            not.BodyLocArgs = bodyLocArgs;
            return not;
        }

        public static GcmNotification WithTitleLocArgs(this GcmNotification not, string titleLocArgs)
        {
            not.TitleLocArgs = titleLocArgs;
            return not;
        }

        public static GcmNotification WithTitleLocKey(this GcmNotification not, string titleLocKey)
        {
            not.TitleLocKey = titleLocKey;
            return not;
        }

    }
}

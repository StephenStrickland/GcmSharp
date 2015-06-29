using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gcm.Net
{
    public class GcmManager
    {

        public GcmManager(string authKey, string senderId, string packageName)
        {
            AuthenticationKey = authKey;
        }

        public GcmManager(GcmManagerOptions opts)
        {
            AuthenticationKey = opts.AuthenticationKey;
            SenderId = opts.SenderId;
            PackageName = opts.PackageName;
        }

        private const string GCM_SEND_URL = "https://gcm-http.googleapis.com/gcm/send";
        private const string GCM_NOTIFICATION_URL = "https://android.googleapis.com/gcm/notification";
        private const int TIMEOUT = 5000;
        private WebRequest request { get; set; }
        private string AuthenticationKey { get; set; }
        private string SenderId { get; set; }
        private string PackageName { get; set; }

        public WebResponse SendMessage(GcmMessage message)
        {
            return Send(message, GCM_SEND_URL);
        }

        public WebResponse CreateDeviceGroupWithRegistrationIds(List<string> ids, string notificationKeyName)
        {
            GcmDeviceGroupOptions deviceGroup = new GcmDeviceGroupOptions()
            {
                NotificationKeyName = notificationKeyName,
                Operation = DeviceGroupOptionsConstants.Create,
                RegistrationIds = ids};
            return Send(deviceGroup, GCM_NOTIFICATION_URL);
        }

        public WebResponse AddRegistrationIdsToDeviceGroup(List<string> ids, string notificationKey, string notificationKeyName = null)
        {
            GcmDeviceGroupOptions deviceGroup = new GcmDeviceGroupOptions() {
                NotificationKeyName = notificationKeyName,
                NotificationKey = notificationKey,
                Operation = DeviceGroupOptionsConstants.Add,
                RegistrationIds = ids};
            return Send(deviceGroup, GCM_NOTIFICATION_URL);
        }

        public WebResponse RemoveRegistrationIdsFromDeviceGroup(List<string> ids, string notificationKey, string notificationKeyName = null)
        {
            GcmDeviceGroupOptions deviceGroup = new GcmDeviceGroupOptions()
            {
                NotificationKeyName = notificationKey,
                NotificationKey = notificationKey,
                Operation = DeviceGroupOptionsConstants.Remove,
                RegistrationIds = ids};
            return Send(deviceGroup, GCM_NOTIFICATION_URL);
        }
       
        public void SetupRequest(string url)
        {
            request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = TIMEOUT;
            request.Proxy = null;
            request.Headers.Add("Authorization: key=" + AuthenticationKey);
        }

        public void CleanupRequest()
        {
        }

        private WebResponse Send(object obj, string url)
        {
            SetupRequest(url);
            string data = JsonConvert.SerializeObject(obj);
            request.ContentLength = data.Length;
            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(data);
                dataStream.Flush();
                dataStream.Close();
            }
            WebResponse response = request.GetResponse();
            return response;
        }
    }

    public class GcmManagerOptions
    {
        public string SenderId { get; set; }
        public string AuthenticationKey { get; set; }
        public string PackageName { get; set; }
    }
}

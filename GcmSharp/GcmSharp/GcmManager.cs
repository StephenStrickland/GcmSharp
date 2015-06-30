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
        private const string GCM_INSTANCE_ID_URL = "https://iid.googleapis.com/iid/info";
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
                RegistrationIds = ids
            };
            return Send(deviceGroup, GCM_NOTIFICATION_URL);
        }

        public WebResponse AddRegistrationIdsToDeviceGroup(List<string> ids, string notificationKey, string notificationKeyName = null)
        {
            GcmDeviceGroupOptions deviceGroup = new GcmDeviceGroupOptions()
            {
                NotificationKeyName = notificationKeyName,
                NotificationKey = notificationKey,
                Operation = DeviceGroupOptionsConstants.Add,
                RegistrationIds = ids
            };
            return Send(deviceGroup, GCM_NOTIFICATION_URL);
        }

        public WebResponse RemoveRegistrationIdsFromDeviceGroup(List<string> ids, string notificationKey, string notificationKeyName = null)
        {
            GcmDeviceGroupOptions deviceGroup = new GcmDeviceGroupOptions()
            {
                NotificationKeyName = notificationKey,
                NotificationKey = notificationKey,
                Operation = DeviceGroupOptionsConstants.Remove,
                RegistrationIds = ids
            };
            return Send(deviceGroup, GCM_NOTIFICATION_URL);
        }

        public GcmInstanceIdRequest SendInstanceIdRequest(bool details, string instanceId)
        {
            SetupGcmInstanceIdRequest();
            GcmInstanceIdRequest instanceIdObj = new GcmInstanceIdRequest();

           // string data = string.Format("{details: \"{0}\", token: \"{1}\" }", details, instanceId);
            return ProcessInstanceId(instanceIdObj, Send(new { details = details, token = instanceId }));
             
        }


        public void SetupGcmMessageRequest(string url)
        {
            request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = TIMEOUT;
            request.Proxy = null;
            request.Headers.Add("Authorization: key=" + AuthenticationKey);
        }


        public void SetupGcmInstanceIdRequest()
        {
            request = WebRequest.Create(GCM_INSTANCE_ID_URL);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = TIMEOUT;
            request.Proxy = null;
            request.Headers.Add("Authorization: key=" + AuthenticationKey);
        }

        public void CleanupRequest()
        {


        }

        //each response types should contain an httpWebResponse

        //make web request async
        //callback

        private T Send( object obj, string url = null)
        {
            if(url != null)
            SetupGcmMessageRequest(url);
            //WebResponse response;
            HttpWebResponse httpWebResponse;
            string data = JsonConvert.SerializeObject(obj);

            string responseData = "";
            request.ContentLength = data.Length;

            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(data);
               // dataStream.Flush();
                dataStream.Close();
            }
            try
            {
                //response = request.GetResponse();
                using(var response = (HttpWebResponse)request.GetResponse())
                {
                    httpWebResponse = response;
                    response.Close();
                }
               // httpWebResponse = (HttpWebResponse)request.GetResponse();
                httpWebResponse.Close();
            }
            catch (WebException ex)
            {
                httpWebResponse = (HttpWebResponse)ex.Response;

                HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);

                using(var reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                }


                ex.Response.Close();
                httpResponse.Close();

                //response = ex.Response;
                //httpWebResponse = httpResponse;
            }


            return httpWebResponse;
        }


        public GcmInstanceIdRequest ProcessInstanceId(GcmInstanceIdRequest req, HttpWebResponse resp)
        {
            req.ResponseStatus = (INSTANCE_ID_RESPONSE_STATUS)resp.StatusCode;
            return req;
        }
    }

    public class GcmManagerOptions
    {
        public string SenderId { get; set; }
        public string AuthenticationKey { get; set; }
        public string PackageName { get; set; }
    }
}

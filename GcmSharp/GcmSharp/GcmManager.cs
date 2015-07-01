using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

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
            ServicePointManager.Expect100Continue = opts.Expect100Continue;
            ServicePointManager.UseNagleAlgorithm = opts.UseNagleAlgorithm;
        }

        private const string GCM_SEND_URL = "https://gcm-http.googleapis.com/gcm/send";
        private const string GCM_NOTIFICATION_URL = "https://android.googleapis.com/gcm/notification";
        private const string GCM_INSTANCE_ID_URL = "https://iid.googleapis.com/iid/info";
        private const int TIMEOUT = 20000;
        private HttpWebRequest request { get; set; }
        private string AuthenticationKey { get; set; }
        private string SenderId { get; set; }
        private string PackageName { get; set; }

        public GcmMessageResponse SendMessage(GcmMessage message)
        {
            GcmMessageResponse response = Send<GcmMessage, GcmMessageResponse>(message, GCM_SEND_URL);
            response.Message = message;
            UpdateResponseStatusForGcmMessage(response);
            return response;
        }

        public GcmDeviceGroupResponse CreateDeviceGroupWithRegistrationIds(List<string> ids, string notificationKeyName)
        {
            GcmDeviceGroupRequest deviceGroup = new GcmDeviceGroupRequest()
            {
                NotificationKeyName = notificationKeyName,
                Operation = DeviceGroupOptionsConstants.Create,
                RegistrationIds = ids
            };
            //(GCM_NOTIFICATION_URL);

            return Send<GcmDeviceGroupRequest, GcmDeviceGroupResponse>(deviceGroup, GCM_NOTIFICATION_URL);
        }

        //public WebResponse AddRegistrationIdsToDeviceGroup(List<string> ids, string notificationKey, string notificationKeyName = null)
        //{
        //    GcmDeviceGroupOptions deviceGroup = new GcmDeviceGroupOptions()
        //    {
        //        NotificationKeyName = notificationKeyName,
        //        NotificationKey = notificationKey,
        //        Operation = DeviceGroupOptionsConstants.Add,
        //        RegistrationIds = ids
        //    };
        //    SetupGcmMessageRequest(GCM_NOTIFICATION_URL);
        //    return Send(deviceGroup);
        //}

        //public WebResponse RemoveRegistrationIdsFromDeviceGroup(List<string> ids, string notificationKey, string notificationKeyName = null)
        //{
        //    GcmDeviceGroupOptions deviceGroup = new GcmDeviceGroupOptions()
        //    {
        //        NotificationKeyName = notificationKey,
        //        NotificationKey = notificationKey,
        //        Operation = DeviceGroupOptionsConstants.Remove,
        //        RegistrationIds = ids
        //    };
        //    SetupGcmMessageRequest(GCM_NOTIFICATION_URL);
        //    return Send(deviceGroup);
        //}

        public GcmInstanceIdResponse GetInstanceIdResponse(bool details, string instanceId)
        {
            GcmInstanceIdResponse response = Send<GcmInstanctIdRequest, GcmInstanceIdResponse>(new GcmInstanctIdRequest() { 
                Details = details, 
                InstanceId = instanceId }, 
                GCM_INSTANCE_ID_URL);
            UpdateResponseStatusForGcmInstanceIdResponse(response);
            return response;
        }


        public void SetupRequest(string url)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = TIMEOUT;
            request.Proxy = null;
            request.Headers.Add("Authorization: key=" + AuthenticationKey);
            request.UserAgent = "GcmSharp(version: 1.0)";
            request.KeepAlive = false;
        }

        private K Send<T, K>(T obj, string url) where K : IResponse, new()
        {
            K response = new K();
            string data = JsonConvert.SerializeObject(obj);

            HttpWebResponse httpResponse;
            string responseData;
            SetupRequest(url);
            request.ContentLength = data.Length;

            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.AutoFlush = false;
                dataStream.Write(data);
                dataStream.Flush();
                dataStream.Close();
            }
            try
            {
                httpResponse = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                }
                httpResponse.Close();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;

                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);

                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                }

                ex.Response.Close();
                httpResponse.Close();
            }

            request.Abort();
            
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<K>(responseData);
            }

            response.HttpWebResponse = httpResponse;
            response.WebRequest = request;

            return response;
        }


        public void UpdateResponseStatusForGcmMessage(GcmMessageResponse response)
        {
            HttpStatusCode statusCode = response.HttpWebResponse.StatusCode;
            if (statusCode == HttpStatusCode.BadRequest)
            {
                response.ResponseStatus = ENUM_GCM_MESSAGE_RESPONSE_TYPES.INVALID_JSON;
            }
            else if(statusCode == HttpStatusCode.OK)
            {
                response.ResponseStatus = ENUM_GCM_MESSAGE_RESPONSE_TYPES.SUCCESS;
                foreach (var status in response.Results)
                {
                    switch (status.Error)
                    {
                        case null:
                            break;
                        case "MissingRegistration":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.MISSING_REGISTRATION_TOKEN;
                            break;
                        case "InvalidRegistration":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.INVALID_REGISTRATION_TOKEN;
                            break;
                        case "NotRegistered":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.UNREGISTED_DEVICE;
                            break;
                        case "InvalidPackageName":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.INVALID_PACKAGE_NAME;
                            break;
                        case "MismatchSenderId":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.MISMATHED_SENDER;
                            break;
                        case "MessageTooBig":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.MESSAGE_TOO_BIG;
                            break;
                        case "InvalidDataKey":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.INVALID_DATA_KEY;
                            break;
                        case "InvalidTtl":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.INVALID_TIME_TO_LIVE;
                            break;
                        case "Unavailable":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.TIMEOUT;
                            response.ResponseStatus = ENUM_GCM_MESSAGE_RESPONSE_TYPES.TIMEOUT;
                            break;
                        case "InternalServerError":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.INTERNAL_SERVER_ERROR;
                            response.ResponseStatus = ENUM_GCM_MESSAGE_RESPONSE_TYPES.INTERNAL_SERVER_ERROR;
                            break;
                        case "DeviceMessageRateExceeded":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.DEVICE_MESSAGE_RATE_EXCEEDED;
                            break;
                        case "TopicsMessageRateExceeded":
                            status.Status = ENUM_GCM_MESSAGE_RESPONSE_TYPES.TOPICS_MESSAGE_RATE_EXCEEDED;
                            break;
                        default:
                            break;
                    }
                }
            }
            else if(statusCode == HttpStatusCode.Unauthorized)
            {
                response.ResponseStatus = ENUM_GCM_MESSAGE_RESPONSE_TYPES.AUTHENTICATION_ERROR;
            }
            else if (statusCode >= HttpStatusCode.InternalServerError)
            {
                response.ResponseStatus = ENUM_GCM_MESSAGE_RESPONSE_TYPES.INTERNAL_SERVER_ERROR;
            }
        }

        private void UpdateResponseStatusForGcmInstanceIdResponse(GcmInstanceIdResponse response)
        {
            switch (response.HttpWebResponse.StatusCode)
            {
                case HttpStatusCode.Forbidden:
                    response.ResponseStatus = INSTANCE_ID_RESPONSE_STATUS.FORBIDDEN;
                    break;
                case HttpStatusCode.Unauthorized:
                    response.ResponseStatus = INSTANCE_ID_RESPONSE_STATUS.UNAUTHORIZED;
                    break;
                case HttpStatusCode.BadRequest: 
                response.ResponseStatus = INSTANCE_ID_RESPONSE_STATUS.BAD_REQUEST;
                break;
                case HttpStatusCode.ServiceUnavailable:
                response.ResponseStatus = INSTANCE_ID_RESPONSE_STATUS.SERVICE_UNAVAILABLE;
                break;
                default:
                    break;
            }

        }


    }

    public class GcmManagerOptions
    {
        public string SenderId { get; set; }
        public string AuthenticationKey { get; set; }
        public string PackageName { get; set; }
        public bool Expect100Continue { get; set; }
        public bool UseNagleAlgorithm { get; set; }
    }
}

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
            GcmMessageResponse response = Send<GcmMessage, GcmMessageResponse>(message);
            response.Message = message;
            UpdateResponseStatusForGcmMessage(response);
            return response;


            //GcmMessageResponse messageResponse = new GcmMessageResponse();
            //string responseData = "";
            //SetupGcmMessageRequest(GCM_SEND_URL);

            //HttpWebResponse httpWebResponse;
            //string data = JsonConvert.SerializeObject(message);

            //request.ContentLength = data.Length;

            //using (var dataStream = new StreamWriter(request.GetRequestStream()))
            //{
            //    dataStream.Write(data);
            //    // dataStream.Flush();
            //    dataStream.Close();
            //}
            //try
            //{
            //    //response = request.GetResponse();
            //    httpWebResponse = (HttpWebResponse)request.GetResponse();

            //    // httpWebResponse = tempResponse;
            //    using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
            //    {
            //        responseData = reader.ReadToEnd();
            //    }
            //    httpWebResponse.Close();
            //    //tempResponse.Close();

            //    // httpWebResponse = (HttpWebResponse)request.GetResponse();
            //}
            //catch (WebException ex)
            //{
            //    httpWebResponse = (HttpWebResponse)ex.Response;

            //    HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
            //    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);

            //    using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
            //    {
            //        responseData = reader.ReadToEnd();
            //    }

            //    ex.Response.Close();
            //    httpResponse.Close();

            //    //response = ex.Response;
            //    //httpWebResponse = httpResponse;
            //}


            //if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            //{
            //    GcmMessageResponse j = JsonConvert.DeserializeObject<GcmMessageResponse>(responseData);
            //    j.Message = message;
            //    j.ResponseStatus = (ENUM_GCM_MESSAGE_RESPONSE_TYPES)httpWebResponse.StatusCode;
            //    j.HttpWebResponse = httpWebResponse;
            //    j.WebRequest = request;
            //    j.ResponseStatus = ENUM_GCM_MESSAGE_RESPONSE_TYPES.SUCCESS;
            //    return j;

            //}

            //httpWebResponse.Close();


            //return messageResponse;
            //return Send(message);
        }

        //public WebResponse CreateDeviceGroupWithRegistrationIds(List<string> ids, string notificationKeyName)
        //{
        //    GcmDeviceGroupOptions deviceGroup = new GcmDeviceGroupOptions()
        //    {
        //        NotificationKeyName = notificationKeyName,
        //        Operation = DeviceGroupOptionsConstants.Create,
        //        RegistrationIds = ids
        //    };
        //    SetupGcmMessageRequest(GCM_NOTIFICATION_URL);

        //    return Send(deviceGroup);
        //}

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
            GcmInstanceIdResponse response = Send<GcmInstanctIdRequest, GcmInstanceIdResponse>(new GcmInstanctIdRequest() { Details = details, InstanceId = instanceId });
            UpdateResponseStatusForGcmInstanceIdResponse(response);
            return response;
            //GcmInstanceIdResponse instanceIdResponse = new GcmInstanceIdResponse();

            //HttpWebResponse httpWebResponse;
            //string data = JsonConvert.SerializeObject(new { details = details, token = instanceId });

            //string responseData = "";


            //SetupGcmInstanceIdRequest();
            //request.ContentLength = data.Length;

            //using (var dataStream = new StreamWriter(request.GetRequestStream()))
            //{
            //    dataStream.Write(data);
            //    // dataStream.Flush();
            //    dataStream.Close();
            //}

            //try
            //{
            //    //response = request.GetResponse();
            //    using (var tempResponse = (HttpWebResponse)request.GetResponse())
            //    {
            //        httpWebResponse = tempResponse;
            //        using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
            //        {
            //            responseData = reader.ReadToEnd();
            //        }
            //        tempResponse.Close();
            //    }
            //    // httpWebResponse = (HttpWebResponse)request.GetResponse();
            //    httpWebResponse.Close();
            //}
            //catch (WebException ex)
            //{
            //    httpWebResponse = (HttpWebResponse)ex.Response;

            //    //HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
            //    Console.WriteLine("Error code: {0}", httpWebResponse.StatusCode);

            //    using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
            //    {
            //        responseData = reader.ReadToEnd();
            //    }

            //    httpWebResponse.Close();
            //    ex.Response.Close();
            //    // httpResponse.Close();

            //    //response = ex.Response;
            //    //httpWebResponse = httpResponse;
            //}



            //// instanceIdResponse.HttpWebResponse =  Send(new { details = details, token = instanceId });
            //instanceIdResponse.HttpWebResponse = httpWebResponse;
            //instanceIdResponse.WebRequest = request;
            //// responseData = ReadData(instanceIdResponse.HttpWebResponse);
            //if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            //{
            //    GcmInstanceIdResponse j = JsonConvert.DeserializeObject<GcmInstanceIdResponse>(data);
            //    j.HttpWebResponse = httpWebResponse;
            //    j.WebRequest = request;
            //    j.ResponseStatus = INSTANCE_ID_RESPONSE_STATUS.SUCCESS;
            //    return j;

            //}

            //Console.WriteLine(request.ToString());

            //instanceIdResponse.ResponseStatus = (INSTANCE_ID_RESPONSE_STATUS)httpWebResponse.StatusCode;
            //// string data = string.Format("{details: \"{0}\", token: \"{1}\" }", details, instanceId);
            //return instanceIdResponse;

        }


        public void SetupGcmMessageRequest(string url)
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


        public void SetupGcmInstanceIdRequest()
        {
            request = WebRequest.Create(GCM_INSTANCE_ID_URL) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = TIMEOUT;
            request.Proxy = null;
            request.Headers.Add("Authorization: key=" + AuthenticationKey);
            request.UserAgent = "GcmSharp(version: 1.0)";
            request.KeepAlive = false;
        }

        public void CleanupRequest()
        {


        }

        //each response types should contain an httpWebResponse

        //make web request async
        //callback


        //private void Send(objectToSend, WebResponse, responseString)
        private K Send<T, K>(T obj) where K : IResponse, new()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            K response = new K();
            GcmMessageResponse messageResponse = new GcmMessageResponse();

            Console.WriteLine("#1 REQUEST SETUP elapsed: {0}", watch.Elapsed);

            HttpWebResponse httpResponse;
            string responseData;

            Console.WriteLine("#2 REQUEST SETUP elapsed: {0}", watch.Elapsed);

            ///if (((GcmMessage)obj) != null)

            if (obj is GcmMessage)
                    SetupGcmMessageRequest(GCM_SEND_URL);

            if (obj is GcmInstanctIdRequest)
                SetupGcmInstanceIdRequest();

            Console.WriteLine("#3 REQUEST SETUP elapsed: {0}", watch.Elapsed);

            //if (tName == typeof(GcmInstanctIdRequest).Name)
            //    SetupGcmInstanceIdRequest();

            string data = JsonConvert.SerializeObject(obj);
            Console.WriteLine("#4 REQUEST SETUP elapsed: {0}", watch.Elapsed);

            //byte[] bytes = Encoding.ASCII.GetBytes(data);
            request.ContentLength = data.Length;
            watch.Stop();
            Console.WriteLine("REQUEST SETUP elapsed: {0}", watch.Elapsed);

            watch.Reset();
            watch.Start();

            //string json = "{ uyfg7yfguyfguyfiuyfiuyfiuyfiuyfiuyfuiyfiuyfiuyfiuyf}"; 

            //request.ContentLength = json.Length;
            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {



                dataStream.AutoFlush = false;
                dataStream.Write(data);
                dataStream.Flush();
                dataStream.Close();
            }

            watch.Stop();
            Console.WriteLine("stream: {0}", watch.Elapsed);

            watch.Reset();
            watch.Start();


            try
            {
                //response = request.GetResponse
                httpResponse = (HttpWebResponse)request.GetResponse();

                // httpWebResponse = tempResponse;
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                }
                httpResponse.Close();
                //tempResponse.Close();

                // httpWebResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;

                //HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);

                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                }

                ex.Response.Close();

                //response = ex.Response;
                //httpWebResponse = httpResponse;
            }
            //finally
            //{
            //    if(httpResponse != null)
            //    httpResponse.Close();

            //}
            request.Abort();
            httpResponse.Close();

            watch.Stop();
            Console.WriteLine("Read elapsed: {0}", watch.Elapsed);

           
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                response  =JsonConvert.DeserializeObject<K>(responseData);
            }
            response.HttpWebResponse = httpResponse;
            response.WebRequest = request;
            //httpResponse.Close();


            return response;
        }


        public GcmInstanceIdResponse ProcessInstanceId(GcmInstanceIdResponse req, HttpWebResponse resp)
        {
            req.ResponseStatus = (INSTANCE_ID_RESPONSE_STATUS)resp.StatusCode;
            return req;
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
    }
}

using Gcm.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string AuthKey = "AIzaSyDZD29PWZQBmSvW0vAYIu0mvkg2ny5TD9A";
            string PackageName = "com.mydealerlot.mdlmobile";
            string SenderId = "595292508198";
            GcmManagerOptions options = new GcmManagerOptions() { 
                AuthenticationKey = AuthKey, 
                PackageName = PackageName,
                SenderId = SenderId
            };
            GcmManager manager = new GcmManager(options);



            GcmMessage message = new GcmMessage().To("janslsnkjdfasdf")
                .WithRestrictedPackageName(PackageName)
                .WithNotification(new GcmNotification().WithBody("body text here").WithTitle("Title Here").WithIcon("myIcon.png").WithColor("color"));




            var r = manager.GetInstanceIdResponse(true, "APA91bEVUAdCF4XdfV6cnecuih5SY48lePF8ZCiBgL8fgqRlB9CKlRyZbqOpfOOfP3SkOG2VYH1LDrYi80aotHg1at6QdcDrk_2t1_Iopw7MAkjv0dD-4DCbyqw7qKpF7INfiRQ2u_Ax");
            Console.WriteLine(r.ToString());
           // manager.SendMessage(message);
            string data = JsonConvert.SerializeObject(message, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
            Console.WriteLine(data);
            Debug.WriteLine(data);
            Console.ReadKey();


        }
    }
}

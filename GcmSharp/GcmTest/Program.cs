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



            GcmMessage message = new GcmMessage().To("APA91bEVUAdCF4XdfV6cnecuih5SY48lePF8ZCiBgL8fgqRlB9CKlRyZbqOpfOOfP3SkOG2VYH1LDrYi80aotHg1at6QdcDrk_2t1_Iopw7MAkjv0dD-4DCbyqw7qKpF7INfiRQ2u_Ax")
                .WithRestrictedPackageName(PackageName).WithTimeToLive(400)
                .WithData(new { body = "Client: MR. SMITH, Appointment Time: today 3:00pm", title = "Your Appointment Has Arrived" });
               // .WithNotification(new GcmNotification().WithBody("body text here").WithTitle("Title Here"));
                //.WithIcon("myIcon.png").WithColor("color"));


            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("getting instance response");
            var r = manager.GetInstanceIdResponse(true, "APA91bEVUAdCF4XdfV6cnecuih5SY48lePF8ZCiBgL8fgqRlB9CKlRyZbqOpfOOfP3SkOG2VYH1LDrYi80aotHg1at6QdcDrk_2t1_Iopw7MAkjv0dD-4DCbyqw7qKpF7INfiRQ2u_Ax");
            Console.WriteLine(r.ToString());

            watch.Stop();
            Console.WriteLine("OVERALL: {0}", watch.Elapsed);
            
            //bool again = true;
            //while(again)
            //{

            //Console.WriteLine("ready?");
            //Console.ReadLine();
            // var response = manager.SendMessage(message);
            string data = JsonConvert.SerializeObject(message, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
            Console.WriteLine(data);
            Debug.WriteLine(data);
            //Console.WriteLine("\n again?");
            //var s = Console.ReadLine();
            //if (s.Contains("no"))
            //    again = false;
            //}
            Console.ReadKey();


        }
    }
}

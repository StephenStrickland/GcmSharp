# GcmSharp
This is a C# library to help all of us .Net guys out with push notifications targeting Google Cloud Messaging.

This is a simple a straight forward library to use, just create an instance of a `GcmManager` class, use fluent methods to create a `GcmMessage` with a `GcmNotification`. Then just call the `SendMessage()` method in `GcmManager` and wait for the response.

All of the entities are up to date (as of July 1, 2015) according to the GCM [Server Ref](https://developers.google.com/cloud-messaging/server-ref)


###Example Code


```C#
GcmManagerOptions options = new GcmManagerOptions() { 
      AuthenticationKey = "Auth Key", 
      PackageName = "Package Name",
      SenderId = "SenderId",
      Expect100Continue = false, //default
      UseNagleAlgorithm = false //default
      };

GcmManager manager = new GcmManager(options);

GcmMessage message = new GcmMessage().To("RegistrationTokenHere")
      .WithData(new { body = "Client: MR. SMITH, Appointment Time: today 3:00pm", title = "Your Appointment Has Arrived" })
      .WithNotification(new GcmNotification().WithBody("body text here").WithTitle("Title Here"));

GcmMessageResponse response = manager.SendMessage(message);

````

###About the Code

First, if you haven't setup GCM for your app go ahead and do so [here](https://developers.google.com/mobile/add)

#####Setup
| Property  | Description  | 
| :------------ |:---------------| 
| `AuthenticationKey`| Google developer API key | 
| `PackageName`| Android app package name        |
| `SenderId` | Project number for you project in the [Google Developer Console](https://console.developers.google.com/project)        |
| `Expect100Continue` | Known to speed up the `WebRequest` class, read about it [here](https://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.expect100continue(v=vs.110).aspx)        |
| `UseNagleAlgorithm` | This algorithm is great for sending larger amounts of data(>4kb) over a network. So for example if you are sending a large object in `GcmMessage.Data` you might experience better response speeds if you set this to true. Read about it [here](https://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.usenaglealgorithm(v=vs.110).aspx)|


##UNDER CONSTRUCTION :)




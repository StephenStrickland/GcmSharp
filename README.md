# GcmSharp
This is a C# library to help all of us .Net guys out with push notifications targeting Google Cloud Messaging.

This is a simple a straight forward library to use, just create an instance of a `GcmManager` class, use fluent methods to create a `GcmMessage` with a `GcmNotification`. Then just call the `SendMessage()` method in `GcmManager` and wait for the response.

All of the entities are up to date (as of July 1, 2015) according to the GCM [Server Ref](https://developers.google.com/cloud-messaging/server-ref)


###Nuget
Check it out [here](https://www.nuget.org/packages/GcmSharp/)

Download it by running:
```
PM> Install-Package GcmSharp 
```
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

###Messages

According to Google a message has to key components(outside of who you are sending it to):
* Notification
* Data

Now the Notification property is a notification that will be automagically be handled by GCM and the Android/iOS device to display your notification. However if you need to handle the notification in a different manner or need to pass along more data to your app, you need to use the Data property which is an object being passed along with the message to the device.

#####Populating the Data property

You can pass in your object by creating a new one (as seen above):
```C#
.WithData(new { body = "Client: MR. SMITH, Appointment Time: today 3:00pm", title = "Your Appointment Has Arrived" });
```

Or pass in a class.
```C#
.WithData(InstanceOfMyClass);
```

If you like to vary naming conventions between C# and Obj C/Java, you can use Json.Net's JsonProperty decorators on your classes properties:
```C#
public class MyClass
{
      [JsonProperty(PropertyName = "foo_bar")]
      public string FooBar { get; set; }
}
```


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




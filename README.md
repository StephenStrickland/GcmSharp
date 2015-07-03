# GcmSharp
This is a C# library to help all of us .Net guys out with push notifications targeting Google Cloud Messaging.

This is a simple a straight forward library to use, just create an instance of a `GcmManager` class, use fluent methods to create a `GcmMessage` with a `GcmNotification`. Then just call the `SendMessage()` method in `GcmManager` and wait for the response.

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


ConvenienceSystem

This project is a simple project for ConvenienceSystems (Getränkekassen, z.B.).

The Project is written in C# and uses Xamarin.Android for the App.

The sub-projects:
* ConvenienceApp: An Android (Xamarin) app client
* ConvenienceClientTest: Test for the Command-Line Client
* ConcenienceConsole/ConvenienceBackend: The logic of the backend of the system
* ConvenienceFormClient: A Client based on WinForms
* ConvenienceServer: The server part of the system
* ConvenienceTest: A (legacy) testing subproject

Known Issues:
* On Mono 3.10. on OS X 10.10 the Winforms will not work du to a bug in Mono

Further requirements:
* MySQL Database (struct will be explained later on... maybe..)
* MySQL .NET Drivers (just google for it, you will find the DLLs)
* Xamarin.Adnroid (for the Adnroid App)

Settings:
The Settings.cs is uploaded as stub only. Just fill in yout credentials/server data/etc.
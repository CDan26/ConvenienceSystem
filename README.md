ConvenienceSystem

This project is a simple project for ConvenienceSystems (Getraenkekassen, e.g.).

The Project is written in C# and uses Xamarin.Android for the App.

== The sub-projects ==
* ConvenienceApp: An Android (Xamarin.Android) App Client
* ConvenienceClientTest: Tests for the Command-Line Client
* ConcenienceConsole/ConvenienceBackend: The logic of the backend of the system
* ConvenienceFormClient: A Client based on WinForms (fully supported by mono)
* ConvenienceServer: The server-side application of the system


== Further requirements ==
* MySQL Database (struct can be found in ConvenienceDB.sql)
* MySQL .NET Drivers (using http://dev.mysql.com/downloads/connector/net/ at the moment)
* Xamarin.Adnroid (for the Android App)

== Settings ==
The Settings.cs is uploaded as stub (*-sample) only. Just fill in yourt credentials/server data/etc.

== Communication structure (main parts) ==
* ConvenienceServer: Methods for fetching data from a MySQL Server (Server-Side)
* ConNetServer: Application listening on port and serving ConvenienceSystem-commands. Gets data from ConvenienceServer (Server-Side)
* ConNetClient: Application connecting with a ConNetServer, sending ConvenienceSystem-commands and fetching corresponding data making it available for user-level applications (client-side)
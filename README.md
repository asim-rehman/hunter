# Hunter
Hunter is a web and mobile app for tracking lost devices and getting device information. It was created as part of a Unversity Degree.

## Technology Stack
* C#
* .Net Core 3 (Web API, MVC & Blazor)
* SQL Server
* Entity Framework
* Xamarin

## Build Instructions
This was built using .Net Core 3.0 Preview6, you will need a Google Maps API Key, which needs to go in _Host file in Hunter.Web.Client. In the appsettings.JSON add your secret key and LIVE connection settings for database, build the project and it should suceeded. If you get errors regarding lowercase componenets for Blazor, then you have a newer .Net Core version intstalled. There will be bugs and possible security issues, but it was more or less built for that purpose to it could be tested. 

## License
MIT License
## Launch Maps Plugin for Xamarin and Windows

Open external maps to a specific geolocation or address in your Xamarin.iOS, Xamarin.Android, Windows, and Xamarin.Forms projects.

### Migrate to: [Xamarin.Essentials](https://docs.microsoft.com/xamarin/essentials/index?WT.mc_id=friends-0000-jamont) or [.NET MAUI](https://learn.microsoft.com/dotnet/maui/platform-integration/appmodel/permissions?WT.mc_id=friends-0000-jamont)

I have been working on Plugins for Xamarin for a long time now. Through the years I have always wanted to create a single, optimized, and official package from the Xamarin team at Microsoft that could easily be consumed by any application. The time is now with [Xamarin.Essentials](https://docs.microsoft.com/xamarin/essentials/index?WT.mc_id=friends-0000-jamont), which offers over 50 cross-platform native APIs in a single optimized package. I worked on this new library with an amazing team of developers and I highly highly highly recommend you check it out. 

Additionally, Xamarin.Essentials is now included in & [.NET MAUI](https://learn.microsoft.com/dotnet/maui/platform-integration/appmodel/permissions?WT.mc_id=friends-0000-jamont).

Due to the functionality being included "in the box" I have decided to officially archive this repo.
       

#### Setup
* Available on NuGet: https://www.nuget.org/packages/Xam.Plugin.ExternalMaps [![NuGet](https://img.shields.io/nuget/v/Xam.Plugin.ExternalMaps.svg?label=NuGet)](https://www.nuget.org/packages/Xam.Plugin.ExternalMaps/)
* Install into your PCL project and Client projects.

Build status: [![Build status](https://ci.appveyor.com/api/projects/status/vpw3xi2qlr41wueo?svg=true)](https://ci.appveyor.com/project/JamesMontemagno/launchmapsplugin)

**Platform Support**

|Platform|Version|
| ------------------- | :------------------: |
|Xamarin.iOS|iOS 7+|
|Xamarin.Android|API 10+|
|Windows 10 UWP|10+|
|Xamarin.Mac|Yes||

#### Usage
There are two methods that you can call to navigate either with the geolocation lat/long or with a full address to go to.

```csharp
    /// <summary>
    /// Navigate to specific latitude and longitude.
    /// </summary>
    /// <param name="name">Label to display</param>
    /// <param name="latitude">Lat</param>
    /// <param name="longitude">Long</param>
    /// <param name="navigationType">Type of navigation</param>
    Task<bool> NavigateTo(string name, double latitude, double longitude, NavigationType navigationType = NavigationType.Default);
    
    /// <summary>
    /// Navigate to an address
    /// </summary>
    /// <param name="name">Label to display</param>
    /// <param name="street">Street</param>
    /// <param name="city">City</param>
    /// <param name="state">Sate</param>
    /// <param name="zip">Zip</param>
    /// <param name="country">Country</param>
    /// <param name="countryCode">Country Code if applicable</param>
    /// <param name="navigationType">Navigation type</param>
    Task<bool> NavigateTo(string name, string street, string city, string state, string zip, string country, string countryCode, NavigationType navigationType = NavigationType.Default);
```

Examples:

```csharp
var success = await CrossExternalMaps.Current.NavigateTo("Xamarin", "394 pacific ave.", "San Francisco", "CA", "94111", "USA", "USA");
var success = await CrossExternalMaps.Current.NavigateTo("Space Needle", 47.6204, -122.3491);
```     


**Platform Tweaks**
* NavigationType only works on iOS and macOS
* Android will try to launch Google Maps first. If it is not installed then it will ask to see if a map apps is installed. If that doesn't work then it will launch the browser.

#### Contributions
Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

#### License
Under MIT, see LICENSE file.


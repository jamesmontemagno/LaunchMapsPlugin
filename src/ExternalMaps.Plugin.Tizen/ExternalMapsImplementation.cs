using System;
using System.Threading.Tasks;
using Plugin.ExternalMaps.Abstractions;
using System.Diagnostics;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace Plugin.ExternalMaps
{
	/// <summary>
	/// Implementation for Feature
	/// </summary>
	public class ExternalMapsImplementation : IExternalMaps
	{
		Map map;
		Pin pin;
		public ExternalMapsImplementation()
		{
			map = new Map
			{
				HeightRequest = 500,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
			};
			pin = new Pin();
		}

		/// <summary>
		/// Navigate to specific latitude and longitude.
		/// </summary>
		/// <param name="name">Label to display</param>
		/// <param name="latitude">Lat</param>
		/// <param name="longitude">Long</param>
		/// <param name="navigationType">Type of navigation</param>
		public Task<bool> NavigateTo(string name, double latitude, double longitude, NavigationType navigationType = NavigationType.Default)
		{
			if (string.IsNullOrWhiteSpace(name))
				name = string.Empty;

			try
			{
				Position position = new Position(latitude, longitude);
				map.Pins.Remove(pin);
				map.MoveToRegion(new MapSpan(position, 7, 7));
				pin.Position = position;
				pin.Label = name;
				pin.Type = PinType.Generic;
				map.Pins.Add(pin);
				var layout = new StackLayout
				{
					Children =
					{
						map,
						new Label{ Text = "latitude : " + latitude + ", longitude : " + longitude }
					}
				};
				Dialog dialog = new Dialog
				{
					Title = "External Maps",
					Subtitle = name,
					Content = layout,
					HorizontalOption = LayoutOptions.CenterAndExpand,
					VerticalOption = LayoutOptions.CenterAndExpand,
				};
				dialog.OutsideClicked += (s, e) =>
				{
					dialog.Hide();
				};
				dialog.Show();
				return Task.FromResult(true);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to launch maps: " + ex);
				return Task.FromResult(false);
			}
		}

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
		public async Task<bool> NavigateTo(string name, string street, string city, string state, string zip, string country, string countryCode, NavigationType navigationType = NavigationType.Default)
		{
			if (string.IsNullOrWhiteSpace(name))
				name = string.Empty;

			if (string.IsNullOrWhiteSpace(street))
				street = string.Empty;

			if (string.IsNullOrWhiteSpace(city))
				city = string.Empty;

			if (string.IsNullOrWhiteSpace(state))
				state = string.Empty;

			if (string.IsNullOrWhiteSpace(zip))
				zip = string.Empty;

			if (string.IsNullOrWhiteSpace(country))
				country = string.Empty;

			try
			{
				var geocoder = new Geocoder();
				var address = street + ", " + city + ", " + state + ", " + zip + ", " + country;
				var location = await geocoder.GetPositionsForAddressAsync(address);
				var latitude = 0.0;
				var longitude = 0.0;
				foreach (var position in location)
				{
					latitude = position.Latitude;
					longitude = position.Longitude;
				}

				return await NavigateTo(name, latitude, longitude, navigationType);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to launch maps: " + ex);
				return false;
			}
		}
	}
}

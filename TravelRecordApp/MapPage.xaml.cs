﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TravelRecordApp
{
    public partial class MapPage : ContentPage
    {
        IGeolocator locator = CrossGeolocator.Current;

        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Center the map at user location once map appears
            // As soon user grants permission
            GetLocation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Stop listening when we navigate away from page
            locator.StopListeningAsync();
        }

        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();

            // Wait until permission was granted
            if(status == PermissionStatus.Granted)
            {
                // Center the map at users location
                // If we don't specify, it will center at rome.
                var location = await Geolocation.GetLocationAsync();

                // Get current location changes
                //var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(new TimeSpan(0,1,0), 100);

                locationsMap.IsShowingUser = true;

                CenterMap(location.Latitude, location.Longitude);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMap(e.Position.Latitude, e.Position.Longitude);
        }

        private void CenterMap(double latitude, double longitude)
        {
        
            locationsMap.MoveToRegion(
                new MapSpan(
                    new Xamarin.Forms.Maps.Position(latitude, longitude),
                    1,
                    1
                )
             );
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            // Generally for Android
            if (status == PermissionStatus.Granted)
            {
                return status;
            }

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // IOS: Prompt user to enable the setting in their phone.
                //await DisplayAlert("Location", "Please allow location access in your Settings.", "OK");
                return status;
            }


            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}


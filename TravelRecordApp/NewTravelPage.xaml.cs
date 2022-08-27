using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using TravelRecordApp.Logic;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing(); 

            IGeolocator locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);

            venueListView.ItemsSource = venues;

        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Result;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    ExperienceTitle = experienceTitle.Text,

                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.distance,
                    Latitude = selectedVenue.geocodes.main.latitude,
                    Longitude = selectedVenue.geocodes.main.longitude,
                    VenueName = selectedVenue.name,                
                };

                // Create connection
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    // Create a table
                    conn.CreateTable<Post>();
                    // Insert a Post
                    int rowsInserted = conn.Insert(post);

                    if(rowsInserted > 0)
                    {
                        await DisplayAlert("Success", "New experience saved!", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Failure", "Oh no! Something went wrong!", "OK");
                    }

                    await Navigation.PushAsync(new HomePage());
                }
            }
            catch(NullReferenceException nre)
            {
                throw new NullReferenceException();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
                       
        }

        void venueListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedVenue = venueListView.SelectedItem as Result;

            experienceTitle.Text = selectedVenue.name;
        }
    }
}


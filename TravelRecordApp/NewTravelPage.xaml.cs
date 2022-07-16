using System;
using System.Collections.Generic;
using SQLite;
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

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Post post = new Post()
            {
                Experience = experienceEntry.Text,
                ExperienceTitle = experienceTitle.Text
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
    }
}


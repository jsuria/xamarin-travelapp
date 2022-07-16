using System;
using System.Collections.Generic;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;

        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();

            this.selectedPost = selectedPost;

            experienceEntry.Text = this.selectedPost.Experience;
            experienceTitle.Text = this.selectedPost.ExperienceTitle;
        }

        async void ToolbarItem_Update_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;
            selectedPost.ExperienceTitle = experienceTitle.Text;

            // create connection
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rowsUpdated = conn.Update(selectedPost);

                if (rowsUpdated > 0)
                {
                    await DisplayAlert("Success", "Experience updated!", "OK");
                }
                else
                {
                    await DisplayAlert("Failure", "Oh no! Something went wrong!", "OK");
                }

                await Navigation.PushAsync(new HomePage());

            }
        }

        async void ToolbarItem_Delete_Clicked(System.Object sender, System.EventArgs e)
        {
            bool willDelete = await DisplayAlert("Confirm Removal", "Are you sure?", "Yes", "No");

            if(willDelete)
            {
                // create connection
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>();
                    int rowsDeleted = conn.Delete(selectedPost);

                    if (rowsDeleted > 0)
                    {
                        await DisplayAlert("Deleted", "Entry removed!", "OK");
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
}


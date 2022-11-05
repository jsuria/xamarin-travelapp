using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class HistoryPage : ContentPage
    {      
        public HistoryPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            /*imageHistory.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.bg-history.jpg", assembly);*/
        }


        // Override the default behaviour
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // isolates the scope of conn within this block
            // preventing use of Close()
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                // If table already exists, this is ignored
                conn.CreateTable<Post>();
                // Gets the posts as a list (LINQ)
                var posts = conn.Table<Post>().ToList();
                postListView.ItemsSource = posts;
            }       
        }

        void postListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            // get the selected list item and retrieve as a post type
            var selectedPost = postListView.SelectedItem as Post;

            if(selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}


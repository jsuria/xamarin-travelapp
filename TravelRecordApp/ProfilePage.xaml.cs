﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            imageProfile.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.bg-profile.jpg", assembly);
        }
    }
}


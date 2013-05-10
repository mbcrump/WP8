using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.System.UserProfile;

namespace LockScreen
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnLockScreenImage_Click_1(object sender, RoutedEventArgs e)
        {
            // Setup lock screen.
            if (!LockScreenManager.IsProvidedByCurrentApplication)
            {
                //If you're not the provider, this call will prompt the user for permission.
                //Calling RequestAccessAsync from a background agent is not allowed.
                await LockScreenManager.RequestAccessAsync();
            }

            if (LockScreenManager.IsProvidedByCurrentApplication)
            {
                Uri imageUri = new Uri("ms-appx:///LockScreen.jpg", UriKind.RelativeOrAbsolute);
                Windows.Phone.System.UserProfile.LockScreen.SetImageUri(imageUri);
            }
        }

        private async void btnShowLockScreenSettings_Click_1(object sender, RoutedEventArgs e)
        {
            // Launch URI for the lock screen settings screen.
            var op = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
        }

        private void btnUpdateNotification_Click_1(object sender, RoutedEventArgs e)
        {
            ShellTile.ActiveTiles.First().Update(
                new FlipTileData()
                {
                    Count = 33,
                    WideBackContent = "I really love Batman!",
                });
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string lockscreenKey = "WallpaperSettings";
            string lockscreenValue = "0";

            bool lockscreenValueExists = NavigationContext.QueryString.TryGetValue(lockscreenKey, out lockscreenValue);

            if (lockscreenValueExists)
            {
                NavigationService.Navigate(new Uri("/LockScreenPage.xaml", UriKind.Relative));
            }
        }
     
    }
}
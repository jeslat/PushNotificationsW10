using System;
using Windows.Networking.PushNotifications;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AppPushNotifications
{
	public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			PushNotificationChannel channel = null;

			try
			{
				channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
				TextBoxUri.Text = channel.Uri;
			}
			catch (Exception ex)
			{
				// Could not create a channel. 
			}
		}
	}
}

using System;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;
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

            //Create the push notification channel
            PushNotificationChannel channel;
            try
            {
                channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                TextBoxUri.Text = channel.Uri;

                //Add handler to manage the incoming push notifications if the app is running
                channel.PushNotificationReceived += OnPushNotification;
            }
            catch (Exception ex)
            {
                // Could not create a channel.
            }
        }

        //Method to manage the incoming push notifications if the app is running
        private async void OnPushNotification(PushNotificationChannel sender, PushNotificationReceivedEventArgs e)
        {
            string notificationContent = string.Empty;

            switch (e.NotificationType)
            {
                case PushNotificationType.Badge:
                    notificationContent = e.BadgeNotification.Content.GetXml();
                    break;

                case PushNotificationType.Tile:
                    notificationContent = e.TileNotification.Content.GetXml();
                    break;

                case PushNotificationType.Toast:
                    notificationContent = e.ToastNotification.Content.GetXml();
                    break;

                case PushNotificationType.Raw:
                    notificationContent = e.RawNotification.Content;
                    break;
            }

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                IncomingNotication.Text = notificationContent;
            });

            e.Cancel = true;
        }
    }
}

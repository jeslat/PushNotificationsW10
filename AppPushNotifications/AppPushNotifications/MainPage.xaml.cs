using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
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

                //Unregister and register the background task for raw notifications
                UnregisterBackgroundTask();
                RegisterBackgroundTask();
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

            Debug.WriteLine("TYPE: " + e.NotificationType + " - NOTIFICATION: " + notificationContent);

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                IncomingNotication.Text = notificationContent;
            });

            //e.Cancel = true;
        }

        private void RegisterBackgroundTask()
        {
            var builder = new BackgroundTaskBuilder();
            builder.Name = "RawBackgroundTask";
            builder.TaskEntryPoint = "RawPushNotifications.RawBackgroundTask";
            builder.SetTrigger(new PushNotificationTrigger());
            try
            {
                BackgroundTaskRegistration task = builder.Register();
            }
            catch (Exception e)
            {

            }
        }

        private bool UnregisterBackgroundTask()
        {
            foreach (var iter in BackgroundTaskRegistration.AllTasks)
            {
                IBackgroundTaskRegistration task = iter.Value;
                if (task.Name == "RawBackgroundTask")
                {
                    task.Unregister(true);
                    return true;
                }
            }
            return false;
        }
    }
}

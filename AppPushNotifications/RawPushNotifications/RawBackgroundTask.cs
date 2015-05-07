using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;

namespace RawPushNotifications
{
    public sealed class RawBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var notification = (RawNotification)taskInstance.TriggerDetails;
            var content = notification.Content;

            Debug.WriteLine("RAW NOTIFICATION: " + content);

            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            var xmlNodeList = toastXml.GetElementsByTagName("text");
            xmlNodeList[0].AppendChild(toastXml.CreateTextNode(content));
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}

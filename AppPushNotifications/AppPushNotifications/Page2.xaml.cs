using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppPushNotifications
{
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace EuroMobileApp.Views
{
    public partial class SideMenuPage : ContentPage
    {
        public SideMenuPage() => InitializeComponent();

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            SideMenuView.State = SideMenuState.LeftMenuShown;
        }

        private void Button_Clicked_1(object sender, System.EventArgs e)
        {
            SideMenuView.State = SideMenuState.RightMenuShown;
        }
    }
}

using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using WinRT.Interop;



namespace PRG1_MAUI_ERP_Activum.WinUI
{

    public partial class App : MauiWinUIApplication
    {

        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
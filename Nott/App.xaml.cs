using Nott.Models;

namespace Nott
{
    public partial class App : Application
    {
        public App(AppSettings settings)
        {
            InitializeComponent();

            MainPage = new AppShell(settings);
        }
    }
}

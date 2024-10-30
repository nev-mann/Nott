using Nott.Models;

namespace Nott
{
    public partial class AppShell : Shell
    {
        private readonly AppSettings settings;

        public AppShell(AppSettings settings)
        {
            this.settings = settings;
            InitializeComponent();
#if ANDROID || IOS
            this.FlyoutBehavior = FlyoutBehavior.Flyout;
#else
    this.FlyoutBehavior = FlyoutBehavior.Locked;
#endif
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            settings.Save();
        }
    }
}

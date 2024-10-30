namespace Nott
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.FlyoutBehavior = FlyoutBehavior.Flyout;
#else
    this.FlyoutBehavior = FlyoutBehavior.Locked;
#endif
        }
    }
}

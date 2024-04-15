namespace BanaBet
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FormPage1), typeof(FormPage1));
            Routing.RegisterRoute(nameof(FormPage2), typeof(FormPage2));
            Routing.RegisterRoute(nameof(FormPage3), typeof(FormPage3));
            Routing.RegisterRoute(nameof(FormPage4), typeof(FormPage4));
            Routing.RegisterRoute(nameof(FormPage5), typeof(FormPage5));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}

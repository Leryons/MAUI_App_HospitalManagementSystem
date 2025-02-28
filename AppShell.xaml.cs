namespace HospitalManagementSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        }
    }
}

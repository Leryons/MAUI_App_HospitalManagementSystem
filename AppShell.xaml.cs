namespace HospitalManagementSystem;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();


        // Register viewpages routes //
        Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        Routing.RegisterRoute(nameof(UserInfoPage), typeof(UserInfoPage));
        Routing.RegisterRoute(nameof(PatientPage), typeof(PatientPage));
    }
}
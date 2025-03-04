namespace HospitalManagementSystem;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif


        //Dependecy Injection //

        //ViewModel
        builder.Services.AddSingleton<DataViewModel>();

        //Database
        builder.Services.AddSingleton<Database>();

        //View
        builder.Services.AddSingleton<SearchPage>();
        builder.Services.AddSingleton<SignUpPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<UserInfoPage>();
        builder.Services.AddSingleton<PatientPage>();

        //Tool
        builder.Services.AddSingleton(AudioManager.Current);


        return builder.Build();
    }
}

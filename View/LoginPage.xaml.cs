namespace HospitalManagementSystem.View;

public partial class LoginPage : ContentPage
{
    private readonly IAudioManager audioManager;
    public LoginPage(IAudioManager audioManager, DataViewModel viewModel)
    {
        InitializeComponent();
        this.audioManager = audioManager;
        this.BindingContext = viewModel;
    }

    private async void PlayMusic(object sender, EventArgs e)
    {
        var player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("forastero_re4.mp3"));

        await Task.Delay(1000);

        player.Play();

        await Task.Delay(10000);

        player.Dispose();
    }

    private async void SectionButton_Clicked(object sender, EventArgs e)
    {

        if (sender == SignUpButton)
        {
            await Shell.Current.GoToAsync("SignUpPage");

            return;
        }

        var dataViewModel = ((DataViewModel)BindingContext);

        await dataViewModel.LoginUserAsync();

        await Task.Delay(1000);

        if (sender == LoginButton)
        {
            if (dataViewModel.CurrentUser.Rol.Equals("Doctor") || dataViewModel.CurrentUser.Rol.Equals("Patient"))
            {
                await Shell.Current.GoToAsync("SearchPage");
            }

            if (dataViewModel.CurrentUser.Rol.Equals("Recepcionist"))
            {
                await Shell.Current.GoToAsync("RecepcionistPage");
            }
        }
    }
}
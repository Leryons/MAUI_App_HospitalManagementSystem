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

    private async void PlayMusic(object sender, EventArgs e) //Play EasterEgg music 
    {
        // Play "El Forastero" // This is a EasterEgg // Just for fun
        var player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("forastero_re4.mp3"));

        await Task.Delay(800);

        player.Play();

        await Task.Delay(10000);

        player.Dispose();
    }

    private async void AccountButton_Clicked(object sender, EventArgs e) // Handle two options to get account  
    {
        try
        {
            var dataViewModel = ((DataViewModel)BindingContext);

            // Go to Sign Up Page
            if (sender == SignUpButton)
            {
                await Shell.Current.GoToAsync(nameof(SignUpPage));
            }

            // Inicialization of LoginUserAsync() method
            if (sender == LoginButton)
            {
                await dataViewModel.LoginUserAsync();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in {ex}");
        }
    }
}
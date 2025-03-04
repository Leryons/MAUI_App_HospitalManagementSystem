namespace HospitalManagementSystem.View;

public partial class SearchPage : ContentPage
{
    public SearchPage(DataViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
    private void AutoComplete_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(UserInfoPage));
    }
}
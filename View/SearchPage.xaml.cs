namespace HospitalManagementSystem.View;

public partial class SearchPage: ContentPage
{
	public SearchPage(DataViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (((DataViewModel)BindingContext).AccessLevelConfirmation())
        {
            autoComplete.ItemsSource = ((DataViewModel)BindingContext).Patients;
            autoComplete.Placeholder = "Search Patients";
        }
        else
        {
            autoComplete.ItemsSource = ((DataViewModel)BindingContext).Doctors;
            autoComplete.Placeholder = "Search Doctors";
        }
    }
}
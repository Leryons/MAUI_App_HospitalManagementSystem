namespace HospitalManagementSystem.View;

public partial class SearchPage : ContentPage
{
    public SearchPage(DataViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
        RecepcionistControls.IsVisible = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var viewModel = ((DataViewModel)BindingContext);

        viewModel.GetDoctors();
        viewModel.GetPatients();

        if (viewModel.CurrentUser.Rol.Equals(nameof(Doctor)))
        {
            autoComplete.ItemsSource = viewModel.Patients;
            autoComplete.Placeholder = "Search Patients";
        }
        else if (viewModel.CurrentUser.Rol.Equals(nameof(Patient)))
        {
            autoComplete.ItemsSource = viewModel.Doctors;
            autoComplete.Placeholder = "Search Doctors";
        }
        else if(viewModel.CurrentUser.Rol.Equals(nameof(Recepcionist)))
        {
            RecepcionistControls.IsVisible = true;
        }
    }

    private void FilterAutoComplete(object sender, EventArgs eventArgs)
    {
        if(sender == PatientSelector)
        {
            autoComplete.ItemsSource = ((DataViewModel)BindingContext).Patients;
            autoComplete.Placeholder = "Search Patients";

            PatientSelector.BackgroundColor = Color.FromRgb(227, 30, 37);
            DoctorSelector.BackgroundColor = Color.FromRgb(255, 255, 255);


            PatientSelectorLabel.TextColor = Color.FromRgb(255, 255, 255);
            DoctorSelectorLabel.TextColor = Color.FromRgb(0, 0, 0);

            PatientSelector.InvalidateMeasure();
            DoctorSelector.InvalidateMeasure();
        }        
        
        if(sender == DoctorSelector)
        {
            autoComplete.ItemsSource = ((DataViewModel)BindingContext).Doctors;
            autoComplete.Placeholder = "Search Doctors";

            DoctorSelector.BackgroundColor = Color.FromRgb(227, 30, 37);
            PatientSelector.BackgroundColor = Color.FromRgb(255, 255, 255);

            DoctorSelectorLabel.TextColor = Color.FromRgb(255, 255, 255);
            PatientSelectorLabel.TextColor = Color.FromRgb(0, 0, 0);

            DoctorSelector.InvalidateMeasure();
            PatientSelector.InvalidateMeasure();
        }
    }

    private void autoComplete_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {

    }
}
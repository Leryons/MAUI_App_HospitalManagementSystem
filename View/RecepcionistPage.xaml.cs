namespace HospitalManagementSystem.View;

public partial class RecepcionistPage : ContentPage
{
    public RecepcionistPage(DataViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    private void ActionButton(object sender, EventArgs e)
    {

        if(sender == SearchInfoButton)
        {
            Shell.Current.GoToAsync(nameof(SearchPage));
        }

        if(sender == AddPatientButton)
        {
            Shell.Current.GoToAsync(nameof(AddPatientPage));
        }

    }

}
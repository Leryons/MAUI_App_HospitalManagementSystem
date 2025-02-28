namespace HospitalManagementSystem.View;

public partial class RecepcionistPage : ContentPage
{
    public RecepcionistPage(DataViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}
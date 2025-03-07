namespace HospitalManagementSystem.View;

public partial class PatientPage : ContentPage
{
	public PatientPage(DataViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;

        NameLabel.Text = $"{viewModel.CurrentUser.Name} {viewModel.CurrentUser.LastName}";

		pdfState.IsChecked = viewModel.IsPdfUploaded;
    }
}
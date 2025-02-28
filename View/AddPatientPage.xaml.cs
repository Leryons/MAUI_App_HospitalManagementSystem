namespace HospitalManagementSystem.View;

public partial class AddPatientPage : ContentPage
{
	public AddPatientPage(DataViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

	private void AddPatientButton_Clicked(object sender, EventArgs e)
	{
		var viewModel = ((DataViewModel)BindingContext);

		viewModel.CurrentUser.Rol = nameof(Patient);

		viewModel.RegisterPatientAsync();
	}
}
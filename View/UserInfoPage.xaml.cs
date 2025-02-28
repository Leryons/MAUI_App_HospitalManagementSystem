namespace HospitalManagementSystem.View;

public partial class UserInfoPage : ContentPage
{
	public UserInfoPage(DataViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		var viewModel = ((DataViewModel)BindingContext);

		if(viewModel.CurrentUser.Rol.Equals(nameof(Patient)))
        {
            NameLabel.Text = viewModel.SelectedDoctor.Name;
			LastNameLabel.Text = viewModel.SelectedDoctor.LastName;
			EmailLabel.Text = viewModel.SelectedDoctor.Email;
			PhoneLabel.Text = viewModel.SelectedDoctor.Phone;
			SpecializationLabel.Text = viewModel.SelectedDoctor.Specialization;
			DepartmentLabel.Text = viewModel.SelectedDoctor.Department;
		}	
		
		if(viewModel.CurrentUser.Rol.Equals(nameof(Doctor)))
		{
            MedicalRecordLabel.IsVisible = true;

            NameLabel.Text = viewModel.SelectedPatient.Name;
			LastNameLabel.Text = viewModel.SelectedPatient.LastName;
			EmailLabel.Text = viewModel.SelectedPatient.Email;
			PhoneLabel.Text = viewModel.SelectedPatient.Phone;
			MedicalRecordLabel.Text = viewModel.SelectedPatient.MedicalRecord;
		}
    }
}
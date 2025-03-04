namespace HospitalManagementSystem.View;

public partial class UserInfoPage : ContentPage
{
	public UserInfoPage(DataViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}
namespace HospitalManagementSystem.View;

public partial class SignUpPage : ContentPage
{
    public SignUpPage(DataViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    // Add new Entry for Doctor Rol //
    Entry Specialization = new()
    {
        Placeholder = "Specialization"
    };

    Entry Department = new()
    {
        Placeholder = "Department"
    };

    private void SelectUserTypeButton(object sender, EventArgs e) // Style the user type selector 
    {
        if (sender == DoctorSelector)
        {
            DoctorSelector.BackgroundColor = Color.FromRgb(227, 30, 37);
            PatientSelector.BackgroundColor = Color.FromRgb(255, 255, 255);

            DoctorSelectorLabel.TextColor = Color.FromRgb(255, 255, 255);
            PatientSelectorLabel.TextColor = Color.FromRgb(0, 0, 0);

            DoctorSelector.InvalidateMeasure();
            PatientSelector.InvalidateMeasure();

            Grid.SetRow(Specialization, 7);
            Grid.SetRow(Department, 8);

            SignUpGrid.Children.Add(Specialization);
            SignUpGrid.Children.Add(Department);
        }

        if (sender == PatientSelector)
        {
            PatientSelector.BackgroundColor = Color.FromRgb(227, 30, 37);
            DoctorSelector.BackgroundColor = Color.FromRgb(255, 255, 255);


            PatientSelectorLabel.TextColor = Color.FromRgb(255, 255, 255);
            DoctorSelectorLabel.TextColor = Color.FromRgb(0, 0, 0);

            PatientSelector.InvalidateMeasure();
            DoctorSelector.InvalidateMeasure();

            SignUpGrid.Children.Remove(Specialization);
            SignUpGrid.Children.Remove(Department);

        }
    }

    private async void SignUpButton_Clicked(object sender, EventArgs e) // When SignUpButton is pressed this method handle the actions 
    {
        var dataViewModel = ((DataViewModel)BindingContext);

        if (dataViewModel.Rol.Equals(nameof(Doctor)))
        {
            Debug.WriteLine("DoctorSelector has been pressed and try to sign");

            Specialization.SetBinding(Entry.TextProperty, "Specialization");
            Department.SetBinding(Entry.TextProperty, "Department");

            await dataViewModel.RegisterUserAsync();
        } 

        if (dataViewModel.Rol.Equals(nameof(Patient)))
        {
            Debug.WriteLine("PatientSelector has been pressed and try to sign");

            await dataViewModel.RegisterUserAsync();
        }
    }
}
namespace HospitalManagementSystem.View;

public partial class SignUpPage : ContentPage
{
    public SignUpPage(DataViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    Entry Specialization = new()
    {
        Placeholder = "Specialization"
    };

    Entry Department = new()
    {
        Placeholder = "Department"
    };

    private void SelectUserTypeButton(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (sender == DoctorSelector)
            {
                DoctorSelector.BackgroundColor = Color.FromRgb(227, 30, 37);
                RecepcionistSelector.BackgroundColor = Color.FromRgb(255, 255, 255);

                DoctorSelectorLabel.TextColor = Color.FromRgb(255, 255, 255);
                RecepcionistSelectorLabel.TextColor = Color.FromRgb(0, 0, 0);

                DoctorSelector.InvalidateMeasure();
                RecepcionistSelector.InvalidateMeasure();

                Grid.SetRow(Specialization, 7);
                Grid.SetRow(Department, 8);

                SignUpGrid.Children.Add(Specialization);
                SignUpGrid.Children.Add(Department);
            }

            if (sender == RecepcionistSelector)
            {
                RecepcionistSelector.BackgroundColor = Color.FromRgb(227, 30, 37);
                DoctorSelector.BackgroundColor = Color.FromRgb(255, 255, 255);


                RecepcionistSelectorLabel.TextColor = Color.FromRgb(255, 255, 255);
                DoctorSelectorLabel.TextColor = Color.FromRgb(0, 0, 0);

                RecepcionistSelector.InvalidateMeasure();
                DoctorSelector.InvalidateMeasure();

                SignUpGrid.Children.Remove(Specialization);
                SignUpGrid.Children.Remove(Department);

            }


        });
    }

    private void SignUpButton_Clicked(object sender, EventArgs e)
    {

        var user = ((DataViewModel)BindingContext);

        if (user.Rol.Equals(nameof(Doctor)))
        {
            Debug.WriteLine("DoctorSelector Has been pressed and try to sign");

            Specialization.SetBinding(Entry.TextProperty, "Specialization");
            Department.SetBinding(Entry.TextProperty, "Department");

            ((DataViewModel)BindingContext).RegisterDoctorAsync();
        }

        if (user.Rol.Equals(nameof(Recepcionist)))
        {
            Debug.WriteLine("RecepcionistSelector Has been pressed and try to sign");

            ((DataViewModel)BindingContext).RegisterRecepcionistAsync();
        }
    }
}

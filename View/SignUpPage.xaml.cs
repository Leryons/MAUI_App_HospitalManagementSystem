namespace HospitalManagementSystem.View;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }

    Entry Specialization = new Entry
    {
        Placeholder = "Specialization"
    };

    Entry Department = new Entry
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

            if (sender == SignUpButton)
            {
                Specialization.SetBinding(Entry.TextProperty, "Specialization");
                Department.SetBinding(Entry.TextProperty, "Department");

                await ((DataViewModel)BindingContext).RegisterDoctorAsync();
            }
        });
    }
}

namespace HospitalManagementSystem.ViewModel;

public partial class DataViewModel : ObservableObject
{
    private readonly Database database;

    public DataViewModel(Database database)
    {
        this.database = database;
        GetPatientsAsync();
    }  
    public DataViewModel()
    {
    }

    //List for Patients
    public ObservableCollection<Patient> Patients { get; set; } = [];

    public async Task GetPatientsAsync() // Get Patients from Database 
    {
        var patientsFromDb = await database.GetPatientsAsync();
        Patients.Clear();

        foreach (var patient in patientsFromDb)
        {
            Patients.Add(patient);
            Debug.WriteLine($"Showing patients in list: {patient.Name} - {patient.LastName}.");
        }
    }




    //   User ViewModel  //

    [ObservableProperty]
    private User _currentUser;

    [ObservableProperty]
    private Patient _selectedPatient;


    //User prop
    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _rol;

    [ObservableProperty]
    private string _phone;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;


    //Doctor prop
    [ObservableProperty]
    private string _specialization;

    [ObservableProperty]
    private string _department;


    //Patient prop
    [ObservableProperty]
    private string _medicalRecord;


    // Define Rol
    [RelayCommand]
    public void IsDoctor()
    {
        Rol = nameof(Doctor);
        Debug.WriteLine($"Now {Rol}.");
    }

    [RelayCommand]
    public void IsPatient()
    {
        Rol = nameof(Patient);
        Debug.WriteLine($"Now {Rol}.");
    }

    public void SetEmptyProperty() // Clean property 
    {
        FirstName = String.Empty;
        LastName = String.Empty;
        Rol = String.Empty;
        Phone = String.Empty;
        Email = String.Empty;
        Password = String.Empty;

        Specialization = String.Empty;
        Department = String.Empty;

        MedicalRecord = String.Empty;
    }

    public async Task LoginUserAsync() // Login 
    {
        try
        {
            var user = await database.LoginUserAsync(Email, Password);

            if (user != null)
            {
                CurrentUser = user;

                Debug.WriteLine($"User {CurrentUser.Name} is logging up. Level Access is {CurrentUser.Rol}.");

                switch (CurrentUser.Rol)
                {
                    case "Doctor":
                        await Shell.Current.GoToAsync(nameof(SearchPage));
                        break;

                    case "Patient":
                        await Shell.Current.GoToAsync(nameof(PatientPage));
                        break;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Invalid credentials", "Ok");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex} in LoginUserAsync() method.");
        }
    }

    public async Task RegisterUserAsync() // Register User 
    {
        try
        {
            var insertToDb = false;

            if (Rol.Equals(nameof(Doctor)))
            {
                Doctor doctor = new(Specialization, Department, Rol, FirstName, LastName, Password, Email, Phone);

                insertToDb = await database.RegisterDoctorAsync(doctor);

            }

            else if (Rol.Equals(nameof(Patient)))
            {
                Patient patient = new(MedicalRecord, Rol, FirstName, LastName, Password, Email, Phone);

                insertToDb = await database.RegisterPatientAsync(patient);
            }

            if (insertToDb)
            {
                await Shell.Current.DisplayAlert("", "Sign up sucessfully", "", "Next");

                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Invalid User", "Ok");

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex} in RegisterUserAsync() method");
        }
    }
}

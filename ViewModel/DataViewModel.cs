using CommunityToolkit.Maui.Storage;

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
    private Patient _currentPatient;

    [ObservableProperty]
    private Patient _selectedPatient;

    [ObservableProperty]
    private bool _isPdfUploaded = false;


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
                        CurrentPatient = (Patient)user;
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
                Patient patient = new(null,null, Rol, FirstName, LastName, Password, Email, Phone);

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


    // Quick way, I have to remake all of these //
    [RelayCommand]
    public async Task AddMedicalDiagnosis() // Insert Medical Diagnosis 
    {
        try
        {
            var patient = await database.GetPatientbyNameAsync(SelectedPatient.Name, SelectedPatient.LastName);

            if (patient != null)
            {
                patient.Diagnosis = SelectedPatient.Diagnosis;

                await database.PatientDiagnosisAsync(patient);
                await Shell.Current.DisplayAlert("", "Diagnosis added successfully", "Ok");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in AddMedicalSymptoms() method: {ex}");
            await Shell.Current.DisplayAlert("Error", $"Failed to add Diagnosis: {ex.Message}", "Ok");
        }
    }


    //   PDF Upload/Download  //
    [RelayCommand]
    private async Task UploadPdfAsync()
    {
        var PdfContent = await PickAndReadPdfAsync();
        if (PdfContent != null)
        {
            await database.UpdatePatientAsync(CurrentUser.Name, PdfContent);

            await Shell.Current.DisplayAlert("", "PDF uploaded successfully", "Ok");

            IsPdfUploaded = true;
        }
    }

    private async Task<byte[]> PickAndReadPdfAsync()
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick a PDF file",
                FileTypes = FilePickerFileType.Pdf,
            });

            if (result != null)
            {
                Debug.WriteLine($"File Name: {result.FileName}");

                using (var stream = await result.OpenReadAsync())
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in PickAndReadPdfAsync() method: {ex}");
            await Shell.Current.DisplayAlert("Error", $"Failed to read PDF: {ex.Message}", "Ok");
        }

        return null;
    }

    [RelayCommand]
    private async Task DownloadPdfAsync() // Download PDF
    {
        try
        {
            var patientPdf = await database.GetPatientbyNameAsync(SelectedPatient.Name, SelectedPatient.LastName);

            if (patientPdf != null)
            {
                using var stream = new MemoryStream(patientPdf.Pdf);
                var path = FileSaver.SaveAsync($"{patientPdf.Name}.pdf", stream, default);

                await Shell.Current.DisplayAlert("", "PDF downloaded successfully", "Ok");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in DownloadPdfAsync() method: {ex}");
            await Shell.Current.DisplayAlert("Error", $"Failed to download PDF: {ex.Message}", "Ok");
        }
    }
}

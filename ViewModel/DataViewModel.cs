﻿using System.Security.Cryptography.X509Certificates;

namespace HospitalManagementSystem.ViewModel;

public partial class DataViewModel : ObservableObject
{
    private readonly Database database;

    public DataViewModel() { }
    public DataViewModel(Database database)
    {
        this.database = database;
    }

    //List for Doctors and Patients
    public ObservableCollection<Doctor> Doctors { get; set; } = [];
    public ObservableCollection<Patient> Patients { get; set; } = [];

    public void GetDoctors()
    {
        var doctorsFromDb = database.GetDoctors();
        Doctors.Clear();

        foreach (var doctor in doctorsFromDb)
        {
            Doctors.Add(doctor);
        }
    }

    public void GetPatients()
    {
        var patientsFromDb = database.GetPatients();
        Patients.Clear();

        foreach(var patient in patientsFromDb)
        {
            Patients.Add(patient);
        }
    }


    //User ViewModel

    [ObservableProperty]
    private User _currentUser;

    [ObservableProperty]
    private Patient _selectedPatient;
    
    [ObservableProperty]
    private Doctor _selectedDoctor;


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


    public async Task LoginUserAsync()
    {
        try
        {
            CurrentUser = await database.LoginUserAsync(Email, Password);

            if (CurrentUser != null)
            {
                Debug.WriteLine($"User {CurrentUser.Name} is logging up. Level Acces is {CurrentUser.Rol}.");

                switch (CurrentUser.Rol)
                {
                    case "Doctor":
                        await Shell.Current.GoToAsync("SearchPage");
                        break;

                    case "Patient":
                        await Shell.Current.GoToAsync("SearchPage");
                        break;

                    case "Recepcionist":
                        await Shell.Current.GoToAsync("RecepcionistPage");
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
            Debug.WriteLine(ex);
        }
    }

    public async Task RegisterDoctorAsync()
    {
        try
        {
            Doctor doctor = new Doctor
            {
                Name = _firstName,
                LastName = _lastName,
                Rol = _rol,
                Phone = _phone,
                Email = _email,
                Password = _password,
                Specialization = _specialization,
                Department = _department,
            };

            var confirmInsert = database.RegisterDoctorAsync(doctor);

            if (confirmInsert)
            {
                Shell.Current.DisplayAlert("Ok", "Successfully registered", "Next");
                Shell.Current.GoToAsync("LoginPage");
            }
            else
            {
                Shell.Current.DisplayAlert("Error", "Error, invalid user", "Accept");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public async Task RegisterRecepcionistAsync()
    {
        try
        {
            Recepcionist recepcionist = new Recepcionist
            {
                Name = _firstName,
                LastName = _lastName,
                Rol = _rol,
                Phone = _phone,
                Email = _email,
                Password = _password
            };

            var confirmInsert = database.RegisterRecepcionistAsync(recepcionist);

            if (confirmInsert)
            {
                Shell.Current.DisplayAlert("Ok", "Successfully registered", "Next");
                Shell.Current.GoToAsync("LoginPage");
            }
            else
            {
                Shell.Current.DisplayAlert("Error", "Error, invalid user", "Accept");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    } 
    
    public async Task RegisterPatientAsync()
    {
        try
        {
            Patient patient = new Patient
            {
                Name = _firstName,
                LastName = _lastName,
                Rol = _rol,
                Phone = _phone,
                Email = _email,
                Password = _password,
                MedicalRecord = _medicalRecord,
            };

            var confirmInsert = database.RegisterPatientAsync(patient);

            if (confirmInsert)
            {
                Shell.Current.DisplayAlert("Ok", "Successfully registered", "Next");
                Shell.Current.GoToAsync("LoginPage");
            }
            else
            {
                Shell.Current.DisplayAlert("Error", "Error, invalid user", "Accept");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }


    [RelayCommand]
    public void IsDoctor()
    {
        Rol = nameof(Doctor);
        Debug.WriteLine(Rol);
    }

    [RelayCommand]
    public void IsRecepcionist()
    {
        Rol = nameof(Recepcionist);
        Debug.WriteLine(Rol);
    }  
    
    [RelayCommand]
    public void IsPatient()
    {
        Rol = nameof(Patient);
        Debug.WriteLine(Rol);
    }
}
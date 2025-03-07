namespace HospitalManagementSystem.Service;

public class Database
{
    private readonly SQLiteAsyncConnection sQLite;

    public Database()
    {
        try
        {      
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "UmbrellaCorp.db");
            sQLite = new SQLiteAsyncConnection(dbPath);
            sQLite.CreateTableAsync<User>().Wait();
            sQLite.CreateTableAsync<Doctor>().Wait();
            sQLite.CreateTableAsync<Patient>().Wait();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        if (sQLite == null)
        {
            throw new InvalidOperationException("SQLite connection is not initialized.");
        }
    }

    public SQLiteAsyncConnection? sQLiteAsyncConnection => sQLite;

    public async Task<List<Doctor>> GetDoctorsAsync() // Fill up Doctors List 
    {
        return await sQLite.Table<Doctor>().ToListAsync();
    }

    public async Task<List<Patient>> GetPatientsAsync() //Fill up Patients List 
    {
        return await sQLite.Table<Patient>().ToListAsync();
    }

    public async Task<User?> LoginUserAsync(string name, string password) // Login 
    {
        var patient = await sQLite.Table<Patient>().Where(p => p.Email == name && p.Password == password)
                                                    .FirstOrDefaultAsync();

        if (patient != null)
            return patient;

        var doctor = await sQLite.Table<Doctor>().Where(d => d.Email == name && d.Password == password)
                                                  .FirstOrDefaultAsync();

        if (doctor != null)
            return doctor;

        return null;
    }

    public async Task<bool> RegisterDoctorAsync(Doctor doctor) // Insert Doctor 
    {
        var doctorExist = await sQLite.Table<Doctor>().Where(d => d.Name == doctor.Name && d.LastName == doctor.LastName)
                                                       .FirstOrDefaultAsync();

        

        if (doctorExist == null)
        { 
            await sQLite.InsertAsync(doctor);

            Debug.WriteLine($"Doctor {doctor.Name} has been registered in the database with the access level: {doctor.Rol}.");

            return true;
        }

        return false;
    }

    public async Task<bool> RegisterPatientAsync(Patient patient) // Insert Patient 
    {
        var patientExist = await sQLite.Table<Doctor>().Where(p => p.Name == patient.Name && p.LastName == patient.LastName)
                                                       .FirstOrDefaultAsync();

        if (patientExist == null)
        {
            await sQLite.InsertAsync(patient);

            Debug.WriteLine($"Patient {patient.Name} has been registered in the database with the access level: {patient.Rol}.");

            return true;
        }

        return false;
    }

    public async Task<bool> UpdatePatientAsync(string firstName, byte[] pdfContent) //Insert PDF to Patient
    {
        var patient = await sQLite.Table<Patient>().Where(p => p.Name == firstName)
                                                       .FirstOrDefaultAsync();

        if (patient != null)
        {
            patient.Pdf = pdfContent;
            await sQLite.UpdateAsync(patient);
            Debug.WriteLine($"Patient {patient.Name} has been updated in the database.");
            return true;
        }

        return false;
    }

    public async Task<bool> PatientDiagnosisAsync(Patient patient)
    {
        var patientExist = await sQLite.Table<Patient>().Where(p => p.Name == patient.Name)
                                                        .FirstOrDefaultAsync();

        if (patientExist != null)
        {
            patientExist.Diagnosis = patient.Diagnosis;

            await sQLite.UpdateAsync(patientExist);

            Debug.WriteLine($"Diagnosis of the Patient {patient.Name} has been inserted in the database.");
            return true;
        }

        return false;
    }

    public async Task<Patient> GetPatientbyNameAsync(string firstName, string lastName)
    {
        return await sQLite.Table<Patient>().Where(p => p.Name == firstName && p.LastName == lastName)
                                      .FirstOrDefaultAsync();
    }
}
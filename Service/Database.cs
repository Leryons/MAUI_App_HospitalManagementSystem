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
}
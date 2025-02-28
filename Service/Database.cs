namespace HospitalManagementSystem.Service;

public class Database
{
    private readonly SQLiteConnection? sQLite;

    public Database()
    {
        try
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "UmbrellaCorp.db");
            sQLite = new SQLiteConnection(dbPath);
            sQLite.CreateTable<User>();
            sQLite.CreateTable<Doctor>();
            sQLite.CreateTable<Recepcionist>();
            sQLite.CreateTable<Patient>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public SQLiteConnection? sQLiteConnection => sQLite;

    public List<Doctor> GetDoctors()
    {
        return sQLite.Table<Doctor>().ToList();
    } 
    
    public List<Patient> GetPatients()
    {
        return sQLite.Table<Patient>().ToList();
    }

    public Task<User?> LoginUserAsync(string name, string password)
    {
        return Task.Run(() =>
        {
            var patient = sQLite?.Table<Patient>().Where(p => p.Email == name && p.Password == password)
                                                  .FirstOrDefault();

            if (patient != null)
                return (User?)patient;

            var recepcionist = sQLite?.Table<Recepcionist>().Where(r => r.Email == name && r.Password == password)
                                                            .FirstOrDefault();

            if (recepcionist != null)
                return (User?)recepcionist;

            var doctor = sQLite?.Table<Doctor>().Where(d => d.Email == name && d.Password == password)
                                                .FirstOrDefault();

            if (doctor != null)
                return (User?)doctor;

            return (User?)null;
        });
    }

    public bool RegisterDoctorAsync(Doctor doctor)
    {
        var doctorExist = sQLite?.Table<Doctor>().Where(d => d.Name == doctor.Name && d.LastName == doctor.LastName)
                                                 .FirstOrDefault();



        if (doctorExist == null)
        {
            sQLite?.Insert(doctor);
            Debug.WriteLine($"Doctor {doctor.Name} has been registered in the database with the access level: {doctor.Rol}.");

            return true;
        }

        return false;
    }

    public bool RegisterRecepcionistAsync(Recepcionist recepcionist)
    {
        var recepcionistExist = sQLite?.Table<Recepcionist>().Where(r => r.Name == recepcionist.Name && r.LastName == recepcionist.LastName)
                                                       .FirstOrDefault();

        if (recepcionistExist == null)
        {
            sQLite?.Insert(recepcionist);
            Debug.WriteLine($"Recepcionist {recepcionist.Name} has been registered in the database with the access level: {recepcionist.Rol}.");

            return true;
        }

        return false;
    } 
    
    public bool RegisterPatientAsync(Patient patient)
    {
        var patientExist = sQLite?.Table<Doctor>().Where(p => p.Name == patient.Name && p.LastName == patient.LastName)
                                                       .FirstOrDefault();

        if (patientExist == null)
        {
            sQLite?.Insert(patient);
            Debug.WriteLine($"Patient {patient.Name} has been registered in the database with the access level: {patient.Rol}.");

            return true;
        }

        return false;
    }

}

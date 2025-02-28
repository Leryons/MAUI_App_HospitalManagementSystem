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
            sQLite.CreateTable<Patient>();
            sQLite.CreateTable<Recepcionist>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public SQLiteConnection? sQLiteConnection => sQLite;

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
        var recepcionistExist = sQLite?.Table<Doctor>().Where(r => r.Name == recepcionist.Name && r.LastName == recepcionist.LastName)
                                                       .FirstOrDefault();

        if (recepcionistExist == null)
        {
            sQLite?.Insert(recepcionist);
            Debug.WriteLine($"Recepcionist {recepcionist.Name} has been registered in the database with the access level: {recepcionist.Rol}.");

            return true;
        }

        return false;
    }
}

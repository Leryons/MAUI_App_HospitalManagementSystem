namespace HospitalManagementSystem.Service;

public class Database
{
    private readonly SQLiteConnection? sQLite;

    public Database()
    {
        try
        {
            sQLite = new SQLiteConnection("UmbrellaCorp.db");
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
        sQLite?.Insert(doctor);

        var doctorExist = sQLite?.Table<Doctor>().Where(d => d.Name == doctor.Name && d.LastName == doctor.LastName)
                                                 .FirstOrDefault();

        Debug.WriteLine($"El Doctor {doctorExist.Name} ha sido registrado y se encuentra en la Base de datos.");

        if(doctorExist != null)
            return true;

        return false;
    }
}

namespace HospitalManagementSystem.Model;

public class Patient : User
{
    public string MedicalRecord { get; set; } = string.Empty;

    public Patient(string medicalRecord, string rol, string name, string lastName, string password, string email, string phone)
    : base(rol, name, lastName, password, email, phone)
    {
        MedicalRecord = medicalRecord; 
    }

    public Patient()
    { }
}

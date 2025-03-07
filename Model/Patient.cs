namespace HospitalManagementSystem.Model;

public class Patient : User
{
    public byte[] Pdf { get; set; } = [];
    public string Diagnosis { get; set; } = string.Empty;

    public Patient(byte[] pdf, string diagnosis, string rol, string name, string lastName, string password, string email, string phone)
    : base(rol, name, lastName, password, email, phone)
    {
        Pdf = pdf;
        Diagnosis = diagnosis;
    }

    public Patient()
    { }
}

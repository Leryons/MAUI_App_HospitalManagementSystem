namespace HospitalManagementSystem.Model;

public class Doctor : User
{
    public string Specialization { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;

    public Doctor(string specialization, string department, string rol, string name, string lastName, string password, string email, string phone)
        : base(rol, name, lastName, password, email, phone)
    {
        Specialization = specialization;
        Department = department;
    }

    public Doctor() 
    { }
}

namespace HospitalManagementSystem.Model;

public class User
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public string Rol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

}
﻿namespace HospitalManagementSystem.Model
{
    public class Doctor : User
    {
        public string Specialization { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
}

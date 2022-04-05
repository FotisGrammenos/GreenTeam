﻿namespace PetShop.Model
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return $"{Name} {Surname}"; } }
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Employee()
        {

        }
        public Employee(string name,string surname,decimal sallaryPerMonth,EmployeeType employeeType)
        {
            Name = name;
            Surname = surname; 
            SallaryPerMonth = sallaryPerMonth;
            EmployeeType = employeeType;
        }
    }
}

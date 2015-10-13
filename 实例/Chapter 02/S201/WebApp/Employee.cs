using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public class Employee
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Gender { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Department { get; private set; }

        public Employee(string id, string name, string gender,DateTime birthDate, string department)
        {
            this.Id = id;
            this.Name = name;
            this.Gender = gender;
            this.BirthDate = birthDate;
            this.Department = department;
        }
    }
}
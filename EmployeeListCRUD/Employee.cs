using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace EmployeeListCRUD
{
    public class Employee : TableEntity
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Department { get; set; }

        // Constructor for AddEmployee
        public Employee(string name, string department)
        {
            string id = Guid.NewGuid().ToString();
            Name = name;
            Id = id;
            RowKey = id;
            Department = department;
            PartitionKey = "employees";
        }

        // Constructor for EditEmployee and DeleteEmployee
        public Employee(string id, string name, string department)
        {
            Name = name;
            Id = id;
            RowKey = id;
            Department = department;
            PartitionKey = "employees";
            ETag = "*";
        }

        // Overload constructor to avoid errors when using the class as a generic type parameter in ReadAllEmployees
        public Employee()
        {
        }
    }

    // Using the Employee : TableEntity class would result in excess data sharing
    public class EmployeeOnlyNameAndId
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public EmployeeOnlyNameAndId(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
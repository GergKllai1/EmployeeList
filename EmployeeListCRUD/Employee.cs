using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace EmployeeListCRUD
{
    public class Employee : TableEntity
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Department { get; set; }

        public Employee(string name, string department)
        {
            string id = Guid.NewGuid().ToString();
            Name = name;
            Id = id;
            RowKey = id;
            Department = department;
            PartitionKey = "employees";
        }

        public Employee(string id, string name, string department)
        {
            Name = name;
            Id = id;
            RowKey = id;
            Department = department;
            PartitionKey = "employees";
            ETag = "*";
        }

        public Employee()
        {
        }
    }

    // Used for showing restricted user data
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
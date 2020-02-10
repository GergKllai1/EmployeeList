using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace EmployeeListCRUD
{
    public class Employee
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public Employee(string name, string department)
        {
            string id = Guid.NewGuid().ToString();
            Name = name;
            Id = id;
            RowKey = id;
            PartitionKey = department;
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

    // Needed for table query
    public class EmployeeTable : TableEntity
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
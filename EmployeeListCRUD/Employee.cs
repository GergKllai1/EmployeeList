using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

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

    public class EmployeeTable : TableEntity
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
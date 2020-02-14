using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeListCRUD
{
    public static class ReadAllEmployees
    {
        [FunctionName("ReadAllEmployees")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            // Invoce Microsoft.WindowsAzure.Storage.Table, which will connect to the employees table
            [Table("employees", Connection = "TableStorageConnection")] CloudTable cloudTable,
            ILogger log)
        {
            // Will use to returned the "serialized" user data
            List<EmployeeOnlyNameAndId> employeeList = new List<EmployeeOnlyNameAndId>();

            // Will query all data
            TableQuery<Employee> rangeQuery = new TableQuery<Employee>();

            // Execute the query and loop through the results
            foreach (Employee entity in
                await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null))
            {
                employeeList.Add(new EmployeeOnlyNameAndId(entity.Name, entity.Id));
            }

            return new OkObjectResult($"{JsonSerializer.Serialize(employeeList)}");
        }
    }
}
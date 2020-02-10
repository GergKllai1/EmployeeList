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
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
             [Table("employees")] CloudTable cloudTable,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<EmployeeOnlyNameAndId> employeeList = new List<EmployeeOnlyNameAndId>();

            TableQuery<EmployeeTable> rangeQuery = new TableQuery<EmployeeTable>();

            // Execute the query and loop through the results
            foreach (EmployeeTable entity in
                await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null))
            {
                employeeList.Add(new EmployeeOnlyNameAndId(entity.Name, entity.Id));
            }

            return new OkObjectResult($"{JsonSerializer.Serialize(employeeList)}");
        }
    }
}
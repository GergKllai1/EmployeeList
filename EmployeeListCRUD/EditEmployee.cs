using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace EmployeeListCRUD
{
    public static class EditEmployee
    {
        [FunctionName("EditEmployee")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
            // Invoce Microsoft.WindowsAzure.Storage.Table, which will connect to the employees table
            [Table("employees", Connection = "TableStorageConnection")] CloudTable cloudTable,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string employeeId = data.Id;
            string updatedEmployeeName = data.Name;
            string updatedEmployeeDepartment = data.Department;

            // TableEntity is required for TableOperation.Replace
            Employee updatedEmployeeData = new Employee(employeeId, updatedEmployeeName, updatedEmployeeDepartment);
            
            // Replace (PUT) the data entry. Trying to update RowKey or PartitionKey would result in error.
            // If that is needed the entity has to be deleted and recreated.
            TableOperation operation = TableOperation.Replace(updatedEmployeeData);

            await cloudTable.ExecuteAsync(operation);

            return new OkObjectResult($"The employee data of {updatedEmployeeName} has been updated!");
        }
    }
}
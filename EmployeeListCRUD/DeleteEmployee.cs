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
    public static class DeleteEmployee
    {
        [FunctionName("DeleteEmployee")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
            // Invoce Microsoft.WindowsAzure.Storage.Table, which will connect to the employees table
            [Table("employees", Connection = "TableStorageConnection")] CloudTable cloudTable,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string employeeId = data.id;
            string employeeNameToBeDeleted = data.name;
            string employeeDepartmentToBeDeleted = data.department;

            Employee employeeToDelete = new Employee(employeeId, employeeNameToBeDeleted, employeeDepartmentToBeDeleted);

            TableOperation operation = TableOperation.Delete(employeeToDelete);

            await cloudTable.ExecuteAsync(operation);

            return new OkObjectResult($"Employee with id: {employeeId} and name: {employeeNameToBeDeleted} has been deleted!");
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace EmployeeListCRUD
{
    public static class EditEmployee
    {
        [FunctionName("EditEmployee")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Table("employees", Connection = "TableStorageConnection")] CloudTable cloudTable,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string employeeId = data.id;
            string updatedEmployeeName = data.name;
            string updatedEmployeeDepartment = data.department;

            Employee updatedEmployeeData = new Employee(employeeId, updatedEmployeeName, updatedEmployeeDepartment);

            TableOperation operation = TableOperation.Replace(updatedEmployeeData);

            await cloudTable.ExecuteAsync(operation);

            return new OkObjectResult($"The employee data of {updatedEmployeeName} has been updated!");
        }
    }
}

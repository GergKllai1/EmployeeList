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
    public static class AddEmployee
    {
        [FunctionName("AddEmployee")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            // Invoce Microsoft.WindowsAzure.Storage.Table, which will connect to the employees table
            [Table("employees", Connection = "TableStorageConnection")] CloudTable employeeData,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string name = data.name;
            string department = data.department;

            // data.name / data.department cannot be used directly
            Employee employeeDataToSave = new Employee(name, department);

            TableOperation operation = TableOperation.Insert(employeeDataToSave);

            await employeeData.ExecuteAsync(operation);

            return new OkObjectResult($"The employee {employeeDataToSave.Name} has been saved successfully!");
        }
    }
}
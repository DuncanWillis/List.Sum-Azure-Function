using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace FunctionApp1Practice
{
    public static class SumOfIntegers
    {
        [FunctionName("SumOfIntegers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];
            //string name = "Poo Poo Face";
            //string name = null;

            var list = new List<int>()
            {
                1,2,3,4
            };
            int sum = list.Sum();
            string name = sum.ToString();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            log.LogInformation(requestBody);

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully."
                : $"The sum of your pre-determined list is {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

using FilePoller.Helpers;
using FilePoller.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilePoller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Order orderObj)
        {
            var fileHelper = new FileHelper();
            // Full file path for the poller
            string path = @"C:\Users\User\Folder\Test.csv";

            // Create a list with the records that were read from the csv file
            var resultData = fileHelper.ReadCSVFile(path);

            // Loop through the read records
            for (int i = 0; i < resultData.Count; i++)
            {
                // If there is a match between the order data from the file and the one from the PUT request
                if (orderObj.OrderNumber == resultData[i].OrderNumber)
                {
                    // Change the values based on the object received from the PUT request
                    resultData[i].CustomerName = orderObj.CustomerName;
                    resultData[i].Fees = orderObj.Fees;
                    resultData[i].OrderStatus = resultData[i].OrderStatus switch
                    {
                        "Submitted" => "Provision started",
                        "Provision started" => "Provision pending",
                        "Provision pending" => "Completed",
                        _ => "Provision pending",
                    };
                }
            }

            // Write the edited data in a new file
            fileHelper.WriteCSVFile(@"C:\Users\User\FolderTest\Test.csv", resultData);

            return Ok("Record updated successfully");
        }
    }
}

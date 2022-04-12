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
            string path = @"C:\Users\User\Folder\Test.csv";

            var resultData = fileHelper.ReadCSVFile(path);

            for (int i = 0; i < resultData.Count; i++)
            {
                if (orderObj.OrderNumber == resultData[i].OrderNumber)
                {
                    resultData[i].CustomerName = orderObj.CustomerName;
                    resultData[i].Fees = orderObj.Fees;
                    switch (resultData[i].OrderStatus)
                    {
                        case "Submitted":
                            resultData[i].OrderStatus = "Provision started";
                            break;
                        case "Provision started":
                            resultData[i].OrderStatus = "Provision pending";
                            break;
                        case "Provision pending":
                            resultData[i].OrderStatus = "Completed";
                            break;
                        default:
                            resultData[i].OrderStatus = "Provision pending";
                            break;
                    }
                }
            }

            fileHelper.WriteCSVFile(@"C:\Users\User\FolderTest\Test.csv", resultData);

            return Ok("Record updated successfully");
        }
    }
}

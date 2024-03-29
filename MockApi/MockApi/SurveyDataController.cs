using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MockApi;

[ApiController]
[Route("[controller]")]
public class SurveyDataController : ControllerBase
{
    [HttpGet("{id:int}")]
    public ApiResponse Get(int id)
    {
        if (id == 1000)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new Error
            {
                Message = "Something went wrong"
            };
        }

        if (id == 3000)
        {
            var rn = new Random().Next(100);
            if (rn % 3 != 0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Error
                {
                    Message = "Something went wrong"
                };
            }
        }
        
        if (id == 2000)
        {
            Thread.Sleep(10000);
        }

        return new Data
        {
            SurveyId = id,
            ResponseCount = new Random().Next(5000)
        };
    }
}
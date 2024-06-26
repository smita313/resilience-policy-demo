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
        
        if (id == 2000)
        {
            Thread.Sleep(10000);
        }

        if (id == 3000)
        {
            var rn = new Random().Next();
            if (rn % 3 != 0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Error
                {
                    Message = "Something went wrong"
                };
            }
        }

        return new Data
        {
            SurveyId = id,
            ResponseCount = 1234
        };
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Este controller se usa para cuando el usuario pone un EndPoint que no existe
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {

        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
        
    }
}
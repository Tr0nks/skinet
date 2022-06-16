using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            
        }
      

        public int StatusCode { get; set; }
        public string Message { get; set; }

         private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request , you have made",
                401 => "Authorized, yo are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side, Errors lead to anger. Angel leads to  hate , hate leads to carrer change",
                _ => null
                        
            };

        }
    }
}
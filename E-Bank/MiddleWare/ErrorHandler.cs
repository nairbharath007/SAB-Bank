using E_Bank.Exceptions;
using E_Bank.Models;
using System.Text.Json;

namespace E_Bank.MiddleWare
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _delegate;

        public ErrorHandler(RequestDelegate requestDelegate)
        {
            _delegate = requestDelegate;
        }


        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
               await _delegate(context);
            }
            catch (UserNotFoundException ue) 
            {
                await HandleException(context, ue);

            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }


        }

        private static Task HandleException(HttpContext httpContext, Exception ex)
        {
            var error = new ErrorResponce(ex.Message);
            if(ex is UserNotFoundException)
            {
                error = new ErrorResponce(ex.Message)
                {
                    Message = ex.Message,
                    StatusCode = ((UserNotFoundException)ex).StatusCode
                };
            }

            var result=JsonSerializer.Serialize(error);

            httpContext.Response.StatusCode=error.StatusCode;
            httpContext.Response.ContentType = "application/json";
            

            return httpContext.Response.WriteAsync(result);

        }
    }
}

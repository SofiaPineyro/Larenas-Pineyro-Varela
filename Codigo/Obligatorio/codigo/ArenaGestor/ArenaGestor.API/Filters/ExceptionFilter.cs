using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ArenaGestor.API.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if ((context.Exception is ArgumentException) || (context.Exception is InvalidOperationException))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Content = context.Exception.Message
                };
                return;
            }

            if (context.Exception is NullReferenceException)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Content = context.Exception.Message
                };
                return;
            }

            context.Result = new ContentResult()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Content = "Internal server error"
            };
        }

    }
}

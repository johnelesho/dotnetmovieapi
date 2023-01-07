using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DotNetMoviesApi.Filters;

public class ParsedBadRequest :IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var result = context.Result as IStatusCodeActionResult;
        if (result == null)
        {
            return;
        }

        if (result.StatusCode == (int)HttpStatusCode.BadRequest)
        {
            var response = new List<string>();
            var badRequestObject = context.Result as BadRequestObjectResult;
            if (badRequestObject.Value is string)
            {
                response.Add(badRequestObject.Value.ToString());
            }
            else
            {
                foreach (var key in context.ModelState.Keys)
                {
                    foreach (var value in context.ModelState[key].Errors)
                    {
                        response.Add($"{key}: {value.ErrorMessage}");
                    }
                }

            }
            context.Result = new BadRequestObjectResult(response);

        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}
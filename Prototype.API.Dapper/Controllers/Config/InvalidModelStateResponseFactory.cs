using Microsoft.AspNetCore.Mvc;
using Prototype.API.Dapper.Extensions;
using Prototype.API.Domain.ApiModels;

namespace Prototype.API.Dapper.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorApiModel(messages: errors);

            return new BadRequestObjectResult(response);
        }
    }
}

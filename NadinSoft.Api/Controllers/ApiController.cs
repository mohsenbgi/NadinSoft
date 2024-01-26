using MediatR;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Domain.Common;

namespace NadinSoft.Api.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;

        protected ApiController(ISender sender) => Sender = sender;

        protected IActionResult HandleFailure(Result result) =>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),
                IValidationResult validationResult =>
                    BadRequest(
                        CreateProblemDetails(
                            "Validation Error", StatusCodes.Status400BadRequest,
                            result.Message,
                            validationResult.Errors)),
                _ =>
                    BadRequest(
                        CreateProblemDetails(
                            "Bad Request",
                            StatusCodes.Status400BadRequest,
                            result.Message))
            };

        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            string error,
            string[]? errors = null) =>
            new()
            {
                Title = title,
                Detail = error,
                Status = status,
                Extensions = { { nameof(errors), errors } }
            };
    }
}

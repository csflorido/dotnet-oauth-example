using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace OauthExample.MyWebApi.Exceptions
{
    public class BadRequestException : Exception
    {
        public ProblemDetails ProblemDetails { get; }

        public HttpStatusCode StatusCode { get; }

        public BadRequestException(ProblemDetails problemDetails) : base(problemDetails.Title)
        {
            if (problemDetails is null)
            {
                throw new ArgumentNullException(nameof(problemDetails));
            }

            ProblemDetails = problemDetails;

            if (ProblemDetails.Status is null)
            {
                StatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                StatusCode = (HttpStatusCode)ProblemDetails.Status;
                ProblemDetails.Status = null;
            }
        }

    }
}

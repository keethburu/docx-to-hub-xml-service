using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Docx2HubSvc {
    public class Docx2HubProblemDetailsFactory : ProblemDetailsFactory
    {
        public Docx2HubProblemDetailsFactory()
        {
        }

        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string title = null, string type = null, string detail = null, string instance = null)
        {
            throw new NotImplementedException();
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string title = null, string type = null, string detail = null, string instance = null)
        {
            throw new NotImplementedException();
        }
    }
}
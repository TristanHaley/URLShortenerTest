// Created by: Haley, Tristan (th185132) on: 25/09/2020 at: 11:24.
// Project: Mercury\Mercury.BlazorInterface
// Copyright: © 2020 NCR. All Rights Reserved.
// Filename: CustomExceptionFilterAttribute.cs

using System;
using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application.json";

            if (context.Exception is ValidationException exception)
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                context.Result                          = new JsonResult(exception.Failures);

                return;
            }

            var code                                         = HttpStatusCode.InternalServerError;
            if (context.Exception is NotFoundException) code = HttpStatusCode.NotFound;

            context.HttpContext.Response.StatusCode = (int) code;
            context.Result = new JsonResult(new
            {
                error = new[]
                {
                    context.Exception.Message
                },
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}
﻿using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services.Exeptions
{

    public class ApiException : Exception
    {
        public ApiErrorResponse ApiErrorResponse { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ApiException(ApiErrorResponse error, HttpStatusCode statusCode) : this(error)
        {
            StatusCode = statusCode;
        }

        public ApiException(ApiErrorResponse error)
        {
            ApiErrorResponse = error;
        }
    }
}

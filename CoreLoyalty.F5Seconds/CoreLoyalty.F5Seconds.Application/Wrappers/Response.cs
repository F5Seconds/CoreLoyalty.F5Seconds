﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(bool succeeded, T data, string message = null, List<string> error = null)
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
            Errors = error;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}

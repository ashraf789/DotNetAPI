using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.MessageResponse
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(T response)
        {
            this.ResponseObject = response;
        }

        public ServiceResponse(T response, string property, string message, bool isError)
        {
            this.ResponseObject = response;
            this.Property = property;
            this.Message = message;
            this.IsError = isError;
        }

        public T ResponseObject { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
    }
}
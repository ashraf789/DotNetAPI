using Common;
using Core.MessageResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Core.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(int code, string message, string version, object detail = null) : base(message)
        {
            resultDetail = new ResultDetail(code, message, version, detail);
        }
        public BusinessRuleException(ResultDetail resultDetails) : base(resultDetails.Message)
        {
            resultDetail = resultDetails;
        }

        public BusinessRuleException(ResultDetail resultDetails, Exception innerException) : base(resultDetails.Message, innerException)
        {
            resultDetail = resultDetails;
        }

        protected BusinessRuleException(string message, string version, Exception innerException = null) : base(message, innerException)
        {
            resultDetail = new ResultDetail(ErrorConstants.Unknown, message, version);
        }
        public ResultDetail resultDetail { get; private set; }
    }

    public class AppHttpException : BusinessRuleException
    {
        public HttpStatusCode HttpCode { get; private set; }
        public AppHttpException(HttpStatusCode code, ResultDetail result, Exception innerException = null) : base(result, innerException)
        {
            HttpCode = code;
        }

        public static AppHttpException BadRequest(int code, string message, string detail = "")
        {
            return BadRequest(new ResultDetail(code, message, detail));
        }

        public static AppHttpException BadRequest(ResultDetail detail)
        {
            return new AppHttpException(HttpStatusCode.OK, detail);
        }
    }
}
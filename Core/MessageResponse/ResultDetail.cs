using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.MessageResponse
{
    public class ResultDetail
    {
        public ResultDetail(int code, string message, string version, object details = null)
        {
            this.Code = code;
            this.Message = message;
            this.Version = version;
            this.Details = details;
        }

        [JsonProperty("code")]
        public int Code { get; }

        [JsonProperty("error")]
        public string Message { get; }

        [JsonProperty("version")]
        public string Version { get; }

        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public object Details { get; }
    }
}
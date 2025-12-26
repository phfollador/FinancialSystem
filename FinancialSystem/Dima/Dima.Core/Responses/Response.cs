using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dima.Core.Responses
{
    public class Response<T>
    {
        private readonly int code;
        private const int defaultStatusCode = 200;
        public T? Data { get; set; }
        public string? Message { get; set; } = string.Empty;
        public bool IsSuccess => code is >= 200 and <= 299;

        [JsonConstructor]
        public Response()
        {
            code = defaultStatusCode;
        }

        public Response(T? data, int code = defaultStatusCode, string? message = null)
        {
            Data = data;
            Message = message;
            this.code = code;
        }
    }
}

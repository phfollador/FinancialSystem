using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Responses
{
    public class Response
    {
        private readonly int code;
        public string? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess => code is >= 200 and <= 299;

        public Response(string data, string message, int code)
        {
            Data = data;
            Message = message;
            this.code = code;
        }
    }
}

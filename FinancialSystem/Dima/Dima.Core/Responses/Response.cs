using System.Text.Json.Serialization;

namespace Dima.Core.Responses
{
    public class Response<T>
    {
        private readonly int code;
        public T? Data { get; set; }
        public string? Message { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsSuccess => code is >= 200 and <= 299;

        [JsonConstructor]
        public Response()
        {
            code = Configuration.defaultStatusCode;
        }

        public Response(T? data, int code = Configuration.defaultStatusCode, string? message = null)
        {
            Data = data;
            Message = message;
            this.code = code;
        }
    }
}

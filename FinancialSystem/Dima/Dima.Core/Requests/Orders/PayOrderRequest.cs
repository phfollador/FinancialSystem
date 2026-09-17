namespace Dima.Core.Requests.Orders
{
    public class PayOrderRequest : Request
    {
        public long Number { get; set; }
        public string ExternalReference { get; set; } = string.Empty;
    }
}

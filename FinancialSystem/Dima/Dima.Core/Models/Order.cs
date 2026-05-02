using Dima.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Models
{
    public class Order
    {
        public long Id { get; set; }
        public string Number { get; set; } = Guid.NewGuid().ToString("N")[..8]; // pega os 8 primeiros caracteres
        public string? ExternalReference { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public EPaymentGateway Gateway { get; set; } = EPaymentGateway.Stripe;
        public EOrderStatus Status { get; set; } = EOrderStatus.WaitingPayment;
        public string UserId { get; set; } = string.Empty;
        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public long VoucherId { get; set; }
        public Voucher Voucher { get; set; } = null!;

        public decimal Total => Product.Price - (Voucher?.Amount ?? 0);
    }
}

using System;

namespace Cobaia.WebApi.Models
{
    public class Order
    {
        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Guid id, DateTime createdDate)
        {
            Id = id;
            CreatedDate = createdDate;
            Status = OrderStatus.Submitted;
        }

        public void Accept()
        {
            Status = OrderStatus.Accepted;
        }
    }
}
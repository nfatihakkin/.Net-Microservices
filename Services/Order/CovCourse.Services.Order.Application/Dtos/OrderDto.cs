using CovCourse.Services.Order.Domain.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovCourse.Services.Order.Application.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get;  set; }
        public Address Address { get;  set; }
        public string BuyerId { get;  set; }
        public  List<OrderItemDto> OrderItems{ get;  set; }
    }
}

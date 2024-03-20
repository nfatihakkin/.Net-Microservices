using CovCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovCourse.Services.Order.Domain.OrderAggragate
{
    public class OrderItem:Entity
    {
        public OrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public decimal Price { get; private set; }
        public void UpdateOrderITem(string productName,string pictureUrl,decimal price)
        {
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

    }
}

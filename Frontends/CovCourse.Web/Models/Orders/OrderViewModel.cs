﻿namespace CovCourse.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
       // public Address Address { get; set; }
        public string BuyerId { get; set; }
        private List<OrderItemViewModel> OrderItems { get; set; }
    }
}
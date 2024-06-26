﻿using CovCourse.Services.Order.Infrastucture;
using CovCourse.Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovCourse.Services.Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _context;

        public CreateOrderMessageCommandConsumer(OrderDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Domain.OrderAggragate.Address(context.Message.Province,context.Message.District,context.Message.Street,context.Message.ZipCode,context.Message.Line);

            Domain.OrderAggragate.Order order = new Domain.OrderAggragate.Order(newAddress, context.Message.BuyerId);

            context.Message.OrderItems.ForEach(x => 
            {
                order.AddOrderItem(x.ProductId,x.ProductName,x.Price,x.PictureUrl);
            });

            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync(); 
        }
    }
}

using CovCourse.Services.Order.Application.Commands;
using CovCourse.Services.Order.Application.Dtos;
using CovCourse.Services.Order.Domain.OrderAggragate;
using CovCourse.Services.Order.Infrastucture;
using CovCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovCourse.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }
        public  async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.AddressDto.Province, request.AddressDto.District, request.AddressDto.Street,request.AddressDto.ZipCode, request.AddressDto.Line);

            Domain.OrderAggragate.Order newOrder = new Domain.OrderAggragate.Order(newAddress, request.BuyerId);

            request.OrderItems.ForEach(o => { newOrder.AddOrderItem(o.ProductId,o.ProductName,o.Price,o.PictureUrl); });

             await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto{ OrderId = newOrder.Id},200);
        }
    }
}

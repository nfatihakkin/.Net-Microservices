using AutoMapper;
using CovCourse.Services.Order.Application.Dtos;
using CovCourse.Services.Order.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovCourse.Services.Order.Application.Mapping
{
    public class CustomMapping:Profile
    {
        public CustomMapping() {

            CreateMap<Domain.OrderAggragate.Order, OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggragate.OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Domain.OrderAggragate.Address, AddressDto>().ReverseMap();
        }

    }
}

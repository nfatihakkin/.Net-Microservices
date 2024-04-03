using CovCourse.Services.Order.Infrastucture;
using CovCourse.Shared.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovCourse.Services.Order.Application.Consumers
{
	public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
	{
		private readonly OrderDbContext _orderDbContext;

		public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
		{
			_orderDbContext = orderDbContext;
		}

		public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
		{
			var orderItems = await _orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToListAsync();

			orderItems.ForEach(x => 
			{
				x.UpdateOrderITem(context.Message.UpdatedName, x.PictureUrl, x.Price);
			});

			_orderDbContext.SaveChanges();
		}
	}
}

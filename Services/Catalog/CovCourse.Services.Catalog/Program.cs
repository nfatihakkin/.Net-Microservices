using CovCourse.Services.Catalog.Dtos;
using CovCourse.Services.Catalog.Services;
using CovCourse.Services.Catalog.Settings;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
	// Default Port : 5672
	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
		{
			host.Username("guest");
			host.Password("guest");
		});
	});
});

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;

});
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});


builder.Services.AddSingleton<IDatabaseSettings>(sp => { 
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));



builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
    var courseService = serviceProvider.GetRequiredService<ICourseService>();

    if (!(await categoryService.GetAllAsync()).Data.Any())
    {
        var category = await categoryService.CreateAsync(new CategoryDto { Name = "Asp.net Core Kursu" });
        await categoryService.CreateAsync(new CategoryDto { Name = "Asp.net Core API Kursu" });

	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

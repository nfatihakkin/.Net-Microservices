using CovCourse.Shared.Dtos;
using Dapper;
using Npgsql;
using System.Data;

namespace CovCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var discount = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE Id = @Id", new { Id = id });

            if (discount <=0)
            {
                return Response<NoContent>.Fail("Unsuccessful !", 400);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(),200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE Id = @Id",new {Id=id})).SingleOrDefault();

            if (discount==null)
            {
                return Response<Models.Discount>.Fail("Discount Not Found !", 400);
            }
            return Response<Models.Discount>.Success(discount,200);
        }

        public async Task<Response<Models.Discount>> GetByUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE UserId = @UserId AND Code=@Code", new { UserId = userId ,code = code})).SingleOrDefault();

            if (discount == null)
            {
                return Response<Models.Discount>.Fail("Discount Not Found !", 400);
            }
            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);
            if (saveStatus>0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Unsuccessful", 404);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("UPDATE discount SET userid=@UserId,rate=@Rate,code=@Code WHERE Id=@Id",
                new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });
            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Unsuccessful", 404);
        }
    }
}

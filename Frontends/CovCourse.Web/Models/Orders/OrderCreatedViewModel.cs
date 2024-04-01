namespace CovCourse.Web.Models.Orders
{
    public class OrderCreatedViewModel
    {
        public int OrderId { get; set; }
        public string Error { get; set; }
        public bool isSuccessful { get; set; }
    }
}

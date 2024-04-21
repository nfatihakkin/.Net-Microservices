namespace CovCourse.Web.Models.Baskets
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        private List<BasketItemViewModel> _basketItems;

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (hasDiscount)
                {
                    _basketItems.ForEach(basketItem =>
                    {
                        var discountPrice = basketItem.Price * ((decimal)DiscountRate.Value / 100);
                        basketItem.AppliedDiscount(Math.Round(basketItem.Price- discountPrice,2));
                    });
                }
                return _basketItems;
            }
            set { _basketItems = value; }
        }
        public decimal TotalPrice { get => _basketItems.Sum(x => x.GetCurrentPrice /** x.Quantity*/); }
        public bool hasDiscount { get => !string.IsNullOrEmpty(DiscountCode) && DiscountRate.HasValue; }
        public void CancelDiscount()
        {
            DiscountRate = null;
            DiscountCode = null;
        }
        public void ApplyDiscount(string code,int rate)
        {
            DiscountRate = rate;
            DiscountCode = code;
        }
    }
}

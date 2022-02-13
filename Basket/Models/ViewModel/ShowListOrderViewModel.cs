namespace Basket.Models.ViewModel
{
    public class ShowListOrderViewModel
    {
        public int OrderDetailsId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }
    }
}

namespace EmedicineEB.Controllers.Models
{
    public class Orders
    {

        public int ID { get; set; }

        public string UserId { get; set; }


        public string OrderNo { get; set; }

        public decimal OrderTotal { get; set; }


        public string OrderStatus { get; set; }
        public string? CustomerName { get; internal set; }
        public string? CreatedOn { get; internal set; }
        public string? MedicineName { get; internal set; }
        public string? Manufacturer { get; internal set; }
        public decimal UnitPrice { get; internal set; }
        public decimal TotalPrice { get; internal set; }
        public int Quantity { get; internal set; }
        public string? ImageUrl { get; internal set; }
    }
}
